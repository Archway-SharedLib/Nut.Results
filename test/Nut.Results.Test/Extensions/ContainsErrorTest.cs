using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test
{
    public class ContainsErrorTest
    {
        [Fact]
        public void 値が一致する場合はtrueが返る()
        {
            var error = new Error();
            Result.Error(error).ContainsError(error).Should().BeTrue();
        }
        
        [Fact]
        public void 値が一致しない場合はfalseが返る()
        {
            var error = new Error();
            Result.Error(error).ContainsError(new Error()).Should().BeFalse();
        }

        [Fact]
        public void 成功の場合はfalseが返る()
        {
            Result.Ok().ContainsError(new Error()).Should().BeFalse();
        }

        [Fact]
        public void Eq_値が一致する場合はtrueが返る()
        {
            var error = new Error();

            Result.Error(error)
                .ContainsError(error, EqualityComparer<IError>.Default)
                .Should().BeTrue();
        }
        
        [Fact]
        public void Eq_値が一致しない場合はfalseが返る()
        {
            var error = new Error();

            Result.Error(error)
                .ContainsError(new Error("Foo"), EqualityComparer<IError>.Default)
                .Should().BeFalse();
        }
        
        [Fact]
        public void Eq_成功の場合はfalseが返る()
        {
            Result.Ok()
                .ContainsError(new Error(),EqualityComparer<IError>.Default)
                .Should().BeFalse();
        }

        [Fact]
        public void Predicate_Predicateがnullの場合は例外が発生する()
        {
            Action act = () => ResultExtensions.ContainsError(
                source: Result.Error(new Error()),
                predicate: null);
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Predicate_Predicateの引数には値が渡される()
        {
            var err = new Error();
            Result.Error(err).ContainsError(e =>
            {
                e.Should().Be(err);
                return true;
            });
        }
        
        [Fact]
        public void Predicate_Predicateがtrueを返せばtrueが返る()
        {
            Result.Error(new Error())
                .ContainsError(v => true).Should().BeTrue();
        }
        
        [Fact]
        public void Predicate_Predicateがfalseを返せばfalseが返る()
        {
            Result.Error(new Error())
                .ContainsError(v => false).Should().BeFalse();
        }
        
        [Fact]
        public void Predicate_成功の場合はfalseが返る()
        {
            Result.Ok()
                .ContainsError(_ => true)
                .Should().BeFalse();
        }
        
        [Fact]
        public void Async_nullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.ContainsError(
                source: null as Task<Result>, 
                new Error());
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public async Task Async_値が一致する場合はtrueが返る()
        {
            var err = new Error();
            (await Result.Error(err).AsTask().ContainsError(err)).Should().BeTrue();
        }
        
        [Fact]
        public async Task Async_値が一致しない場合はfalseが返る()
        {
            var err = new Error();
            (await Result.Error(err).AsTask().ContainsError(new Error()))
                .Should().BeFalse();
        }
        
        [Fact]
        public async Task Async_成功の場合はfalseが返る()
        {
            (await Result.Ok().AsTask().ContainsError(new Error()))
                .Should().BeFalse();
        }
        
        [Fact]
        public void Async_Eq_nullの場合は例外が発生する()
        {
            Func<Task> act = () => 
                ResultExtensions.ContainsError(
                    source: null as Task<Result>,
                    new Error(),
                    EqualityComparer<IError>.Default);
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public async Task Async_Eq_値が一致する場合はtrueが返る()
        {
            var err = new Error();
            (await Result.Error(err).AsTask()
                .ContainsError(err, EqualityComparer<IError>.Default))
                .Should().BeTrue();
        }
        
        [Fact]
        public async Task Async_Eq_値が一致しない場合はfalseが返る()
        {
            var err = new Error();
            (await Result.Error(err).AsTask()
                    .ContainsError(new Error(), EqualityComparer<IError>.Default))
                .Should().BeFalse();
        }
        
        [Fact]
        public async Task Async_Eq_成功の場合はfalseが返る()
        {
            (await Result.Ok().AsTask()
                .ContainsError(new Error(), EqualityComparer<IError>.Default))
                .Should().BeFalse();
        }
        
        [Fact]
        public void Async_Predicate_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.ContainsError(
                source: null as Task<Result>,
                predicate: _ => true);
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Async_Predicate_Predicateがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.ContainsError(
                source: Result.Ok().AsTask(),
                predicate: null);
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public async Task Async_Predicate_Predicateの引数には値が渡される()
        {
            var err = new Error();
            await Result.Error(err).AsTask().ContainsError(e =>
            {
                e.Should().Be(err);
                return true;
            });
        }
        
        [Fact]
        public async Task Async_Predicate_Predicateがtrueを返せばtrueが返る()
        {
            (await Result.Error(new Error()).AsTask().ContainsError(v => true))
                .Should().BeTrue();
        }
        
        [Fact]
        public async Task Async_Predicate_Predicateがfalseを返せばfalseが返る()
        {
            (await Result.Error(new Error()).AsTask().ContainsError(v => false))
                .Should().BeFalse();
        }
        
        [Fact]
        public async Task Async_Predicate_成功の場合はfalseが返る()
        {
            (await Result.Ok().AsTask()
                .ContainsError(_ => true))
                .Should().BeFalse();
        }
    }
}