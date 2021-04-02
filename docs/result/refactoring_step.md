# How to use Nut.Results from refactoring

In this section, we will refactor some common dangerous codes using Nut.Results.The refactoring code is placed in the `sample` directory, so please refer to it.

## 0. Initial code

The sample stories to be refactored are as follows.

1. The service retrieves data from the repository with the specified ID.
2. The service updates the retrieved data with the value specified in the argument.
3. The updated value is saved using the repository.
4. The value returned as a result of the storage is returned to the caller.

The code for this process is as follows.

```cs
public class UserService
{
    public string UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var user = repository.GetUserById(userId);
        user.Name = name;
        return repository.Save(user);
    }
}
```

The implementation of the repository is as follows.

```cs
public class UserRepository
{
    public User GetUserById(string userId)
    {
        return null;
    }

    public string Save(User user)
    {
        return user.Id;
    }
}
```

This code has the following problems.

- The repository is returning null, which causes an exception.
- The meaning of the null value returned from the repository cannot be determined whether there is no data or some error has occurred.

To deal with this problem, the repository will be changed to return `Result{T}`.

## 1. Specify the result by `Result{T}`

The repository has been changed to return `Result{T}`. Also, the fact that there is no data is made clear by returning the type `DatanotFoundError`.

```cs
public class UserRepository
{
    public Result<User> GetUserById(string userId)
    {
        return Result.Error<User>(new DataNotFoundError());
    }

    public Result<string> Save(User user)
    {
        // Automatically converted to Result.
        return user.Id;
    }
}
```

Change the `UserService` as follows.

```cs
public class UserService
{
    public string UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId); //　return Result
        var user = result.Get();
        user.Name = name;
        var saveResult = repository.Save(user);
        return saveResult.Get();
    }
}
```

However, this also has the following problems.

- The `Result.Get` method raises an exception in case of failure.

The `Result.Get` method will issue an `InvalidOperationException` if the state is a failure. This is a specification to make it clear that this is a bug in the implementation that the state must be checked.

To deal with this problem, change the result to check for succeeded or not.

## 2. Check the Result status.

Changed to use the `IsOk` property to check before the `Get` method.

```cs
public class UserService
{
    public string UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId);
        if(result.IsOk)
        {
            var user = result.Get();
            user.Name = name;
            var saveResult = repository.Save(user);
            if(saveResult.IsOk)
            {
                return saveResult.Get();
            }
        }
        return null;
    }
}
```

This change ensures that the process is executed in case of success and in case of failure. However, it returned `null` in case of failure, which caused the following problem.

- The caller of this method receives `null` and does not understand the meaning of `null`.
- The information that there is no data returned from the repository has disappeared.

To deal with this problem, we will change the service to return `Result{T}` as well.

## 3. The service returns a Result.

The return value of the service method has been changed to `Result{T}`.

```cs
public class UserService
{
    public Result<string> UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId);
        if (result.IsOk)
        {
            var user = result.Get();
            user.Name = name;
            var saveResult = repository.Save(user);
            if (saveResult.IsOk)
            {
                return saveResult.Get();
            }
        }
        return Result.Error<string>(result.GetError());
    }
}
```

Now, the cause of failure can be made clear to the caller, and the caller and others can be assured of the process. However, this code has the following problems that reduce its readability.

- The if statement is nested and is redundant.
- Errors are being re-stuffed, resulting in redundancy.

To deal with this problem, change to use the extension methods provided for `Result` and `Result{T}`.

## 4. Use extension methods

Use the `Map` and `FlatMap` methods to remove the conditional branching.

```cs
public class UserService
{
    public Result<string> UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId);
        return result.Map(user =>
        {
            user.Name = name;
            return user;
        }).FlatMap(user =>
        {
            return repository.Save(user);
        }); 
    }
}
```

So far, the refactoring is almost complete. The last thing to do is to remove unnecessary variables.

## 5. Remove unnecessary variables

The following changes have been made

- Remove the `result` variable as it is unnecessary and redundant.
- Remove the parameter of `FlatMap` method because it can specify the `Save` method.

```cs
public class UserService
{
    public Result<string> UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        return repository.GetUserById(userId).Map(user =>
        {
            user.Name = name;
            return user;
        }).FlatMap(repository.Save);
    }
}
```

This is the end of the refactoring. During the refactoring process, there were some parts where the code became long due to the insertion of checks and other processes, and there were also some parts where readability was reduced due to nesting. However, by utilizing the extension methods, the code became linear and simple in the end.