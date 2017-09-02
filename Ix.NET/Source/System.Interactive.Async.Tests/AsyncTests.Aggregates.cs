﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantTypeArgumentsOfMethod

namespace Tests
{
    public partial class AsyncTests
    {
        private const int WaitTimeoutMs = 5000;

        [Fact]
        public async Task IsEmpty_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.IsEmpty<int>(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.IsEmpty<int>(null, CancellationToken.None));
        }

        [Fact]
        public async Task First_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.First<int>(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.First<int>(null, x => true));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.First<int>(AsyncEnumerable.Return(42), default(Func<int, bool>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.First<int>(null, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.First<int>(null, x => true, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.First<int>(AsyncEnumerable.Return(42), default(Func<int, bool>), CancellationToken.None));
        }

        [Fact]
        public void First1()
        {
            var res = AsyncEnumerable.Empty<int>().First();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void First2()
        {
            var res = AsyncEnumerable.Empty<int>().First(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void First3()
        {
            var res = AsyncEnumerable.Return(42).First(x => x % 2 != 0);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void First4()
        {
            var res = AsyncEnumerable.Return(42).First();
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void First5()
        {
            var res = AsyncEnumerable.Return(42).First(x => x % 2 == 0);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void First6()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).First();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void First7()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).First(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void First8()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().First();
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void First9()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().First(x => x % 2 != 0);
            Assert.Equal(45, res.Result);
        }

        [Fact]
        public async Task FirstOrDefault_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.FirstOrDefault<int>(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.FirstOrDefault<int>(null, x => true));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.FirstOrDefault<int>(AsyncEnumerable.Return(42), default(Func<int, bool>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.FirstOrDefault<int>(null, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.FirstOrDefault<int>(null, x => true, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.FirstOrDefault<int>(AsyncEnumerable.Return(42), default(Func<int, bool>), CancellationToken.None));
        }

        [Fact]
        public void FirstOrDefault1()
        {
            var res = AsyncEnumerable.Empty<int>().FirstOrDefault();
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void FirstOrDefault2()
        {
            var res = AsyncEnumerable.Empty<int>().FirstOrDefault(x => true);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void FirstOrDefault3()
        {
            var res = AsyncEnumerable.Return(42).FirstOrDefault(x => x % 2 != 0);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void FirstOrDefault4()
        {
            var res = AsyncEnumerable.Return(42).FirstOrDefault();
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void FirstOrDefault5()
        {
            var res = AsyncEnumerable.Return(42).FirstOrDefault(x => x % 2 == 0);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void FirstOrDefault6()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).FirstOrDefault();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void FirstOrDefault7()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).FirstOrDefault(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void FirstOrDefault8()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().FirstOrDefault();
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void FirstOrDefault9()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().FirstOrDefault(x => x % 2 != 0);
            Assert.Equal(45, res.Result);
        }

        [Fact]
        public void FirstOrDefault10()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().FirstOrDefault(x => x < 10);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public async Task Last_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Last<int>(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Last<int>(null, x => true));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Last<int>(AsyncEnumerable.Return(42), default(Func<int, bool>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Last<int>(null, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Last<int>(null, x => true, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Last<int>(AsyncEnumerable.Return(42), default(Func<int, bool>), CancellationToken.None));
        }

        [Fact]
        public void Last1()
        {
            var res = AsyncEnumerable.Empty<int>().Last();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void Last2()
        {
            var res = AsyncEnumerable.Empty<int>().Last(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void Last3()
        {
            var res = AsyncEnumerable.Return(42).Last(x => x % 2 != 0);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void Last4()
        {
            var res = AsyncEnumerable.Return(42).Last();
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void Last5()
        {
            var res = AsyncEnumerable.Return(42).Last(x => x % 2 == 0);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void Last6()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).Last();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Last7()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).Last(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Last8()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().Last();
            Assert.Equal(90, res.Result);
        }

        [Fact]
        public void Last9()
        {
            var res = new[] { 42, 23, 45, 90 }.ToAsyncEnumerable().Last(x => x % 2 != 0);
            Assert.Equal(45, res.Result);
        }

        [Fact]
        public async Task LastOrDefault_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.LastOrDefault<int>(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.LastOrDefault<int>(null, x => true));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.LastOrDefault<int>(AsyncEnumerable.Return(42), default(Func<int, bool>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.LastOrDefault<int>(null, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.LastOrDefault<int>(null, x => true, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.LastOrDefault<int>(AsyncEnumerable.Return(42), default(Func<int, bool>), CancellationToken.None));
        }

        [Fact]
        public void LastOrDefault1()
        {
            var res = AsyncEnumerable.Empty<int>().LastOrDefault();
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void LastOrDefault2()
        {
            var res = AsyncEnumerable.Empty<int>().LastOrDefault(x => true);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void LastOrDefault3()
        {
            var res = AsyncEnumerable.Return(42).LastOrDefault(x => x % 2 != 0);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void LastOrDefault4()
        {
            var res = AsyncEnumerable.Return(42).LastOrDefault();
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void LastOrDefault5()
        {
            var res = AsyncEnumerable.Return(42).LastOrDefault(x => x % 2 == 0);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void LastOrDefault6()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).LastOrDefault();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void LastOrDefault7()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).LastOrDefault(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void LastOrDefault8()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().LastOrDefault();
            Assert.Equal(90, res.Result);
        }

        [Fact]
        public void LastOrDefault9()
        {
            var res = new[] { 42, 23, 45, 90 }.ToAsyncEnumerable().LastOrDefault(x => x % 2 != 0);
            Assert.Equal(45, res.Result);
        }

        [Fact]
        public void LastOrDefault10()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().LastOrDefault(x => x < 10);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public async Task Single_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Single<int>(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Single<int>(null, x => true));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Single<int>(AsyncEnumerable.Return(42), default(Func<int, bool>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Single<int>(null, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Single<int>(null, x => true, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.Single<int>(AsyncEnumerable.Return(42), default(Func<int, bool>), CancellationToken.None));
        }

        [Fact]
        public void Single1()
        {
            var res = AsyncEnumerable.Empty<int>().Single();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void Single2()
        {
            var res = AsyncEnumerable.Empty<int>().Single(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void Single3()
        {
            var res = AsyncEnumerable.Return(42).Single(x => x % 2 != 0);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void Single4()
        {
            var res = AsyncEnumerable.Return(42).Single();
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void Single5()
        {
            var res = AsyncEnumerable.Return(42).Single(x => x % 2 == 0);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void Single6()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).Single();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Single7()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).Single(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Single8()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().Single();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void Single9()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().Single(x => x % 2 != 0);
            Assert.Equal(45, res.Result);
        }

        [Fact]
        public void Single10()
        {
            var res = new[] { 42, 23, 45, 90 }.ToAsyncEnumerable().Single(x => x % 2 != 0);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void Single11()
        {
            var res = new int[0].ToAsyncEnumerable().Single();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public async Task SingleOrDefault_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SingleOrDefault<int>(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SingleOrDefault<int>(null, x => true));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SingleOrDefault<int>(AsyncEnumerable.Return(42), default(Func<int, bool>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SingleOrDefault<int>(null, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SingleOrDefault<int>(null, x => true, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SingleOrDefault<int>(AsyncEnumerable.Return(42), default(Func<int, bool>), CancellationToken.None));
        }

        [Fact]
        public void SingleOrDefault1()
        {
            var res = AsyncEnumerable.Empty<int>().SingleOrDefault();
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void SingleOrDefault2()
        {
            var res = AsyncEnumerable.Empty<int>().SingleOrDefault(x => true);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void SingleOrDefault3()
        {
            var res = AsyncEnumerable.Return(42).SingleOrDefault(x => x % 2 != 0);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void SingleOrDefault4()
        {
            var res = AsyncEnumerable.Return(42).SingleOrDefault();
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void SingleOrDefault5()
        {
            var res = AsyncEnumerable.Return(42).SingleOrDefault(x => x % 2 == 0);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void SingleOrDefault6()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).SingleOrDefault();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void SingleOrDefault7()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).SingleOrDefault(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void SingleOrDefault8()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().SingleOrDefault();
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void SingleOrDefault9()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().SingleOrDefault(x => x % 2 != 0);
            Assert.Equal(45, res.Result);
        }

        [Fact]
        public void SingleOrDefault10()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().SingleOrDefault(x => x < 10);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void SingleOrDefault11()
        {
            var res = new[] { 42, 45, 90 }.ToAsyncEnumerable().SingleOrDefault(x => true);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void SingleOrDefault12()
        {
            var res = new int[0].ToAsyncEnumerable().SingleOrDefault();
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public async Task ElementAt_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ElementAt<int>(null, 0));
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => AsyncEnumerable.ElementAt<int>(AsyncEnumerable.Return(42), -1));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ElementAt<int>(null, 0, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => AsyncEnumerable.ElementAt<int>(AsyncEnumerable.Return(42), -1, CancellationToken.None));
        }

        [Fact]
        public void ElementAt1()
        {
            var res = AsyncEnumerable.Empty<int>().ElementAt(0);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is ArgumentOutOfRangeException);
        }

        [Fact]
        public void ElementAt2()
        {
            var res = AsyncEnumerable.Return<int>(42).ElementAt(0);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void ElementAt3()
        {
            var res = AsyncEnumerable.Return<int>(42).ElementAt(1);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is ArgumentOutOfRangeException);
        }

        [Fact]
        public void ElementAt4()
        {
            var res = new[] { 1, 42, 3 }.ToAsyncEnumerable().ElementAt(1);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void ElementAt5()
        {
            var res = new[] { 1, 42, 3 }.ToAsyncEnumerable().ElementAt(7);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is ArgumentOutOfRangeException);
        }

        [Fact]
        public void ElementAt6()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).ElementAt(15);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public async Task ElementAtOrDefault_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ElementAtOrDefault<int>(null, 0));
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => AsyncEnumerable.ElementAtOrDefault<int>(AsyncEnumerable.Return(42), -1));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ElementAtOrDefault<int>(null, 0, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => AsyncEnumerable.ElementAtOrDefault<int>(AsyncEnumerable.Return(42), -1, CancellationToken.None));
        }

        [Fact]
        public void ElementAtOrDefault1()
        {
            var res = AsyncEnumerable.Empty<int>().ElementAtOrDefault(0);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void ElementAtOrDefault2()
        {
            var res = AsyncEnumerable.Return<int>(42).ElementAtOrDefault(0);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void ElementAtOrDefault3()
        {
            var res = AsyncEnumerable.Return<int>(42).ElementAtOrDefault(1);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void ElementAtOrDefault4()
        {
            var res = new[] { 1, 42, 3 }.ToAsyncEnumerable().ElementAtOrDefault(1);
            Assert.Equal(42, res.Result);
        }

        [Fact]
        public void ElementAtOrDefault5()
        {
            var res = new[] { 1, 42, 3 }.ToAsyncEnumerable().ElementAtOrDefault(7);
            Assert.Equal(0, res.Result);
        }

        [Fact]
        public void ElementAtOrDefault6()
        {
            var ex = new Exception("Bang!");
            var res = AsyncEnumerable.Throw<int>(ex).ElementAtOrDefault(15);
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public async Task ToDictionary_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(default(IAsyncEnumerable<int>), x => 0));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(AsyncEnumerable.Return(42), default(Func<int, int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(default(IAsyncEnumerable<int>), x => 0, EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(AsyncEnumerable.Return(42), x => 0, default(IEqualityComparer<int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(default(IAsyncEnumerable<int>), x => 0, x => 0));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), x => 0));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), x => 0, default(Func<int, int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(default(IAsyncEnumerable<int>), x => 0, x => 0, EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), x => 0, EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), x => 0, default(Func<int, int>), EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), x => 0, x => 0, default(IEqualityComparer<int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(default(IAsyncEnumerable<int>), x => 0, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), CancellationToken.None));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(default(IAsyncEnumerable<int>), x => 0, EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int>(AsyncEnumerable.Return(42), x => 0, default(IEqualityComparer<int>), CancellationToken.None));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(default(IAsyncEnumerable<int>), x => 0, x => 0, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), x => 0, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), x => 0, default(Func<int, int>), CancellationToken.None));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(default(IAsyncEnumerable<int>), x => 0, x => 0, EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), x => 0, EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), x => 0, default(Func<int, int>), EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToDictionary<int, int, int>(AsyncEnumerable.Return(42), x => 0, x => 0, default(IEqualityComparer<int>), CancellationToken.None));
        }

        [Fact]
        public void ToDictionary1()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = xs.ToDictionary(x => x % 2).Result;
            Assert.True(res[0] == 4);
            Assert.True(res[1] == 1);
        }

        [Fact]
        public void ToDictionary2()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            AssertThrows<Exception>(() => xs.ToDictionary(x => x % 2).Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is ArgumentException);
        }

        [Fact]
        public void ToDictionary3()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = xs.ToDictionary(x => x % 2, x => x + 1).Result;
            Assert.True(res[0] == 5);
            Assert.True(res[1] == 2);
        }

        [Fact]
        public void ToDictionary4()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            AssertThrows<Exception>(() => xs.ToDictionary(x => x % 2, x => x + 1).Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is ArgumentException);
        }

        [Fact]
        public void ToDictionary5()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = xs.ToDictionary(x => x % 2, new Eq()).Result;
            Assert.True(res[0] == 4);
            Assert.True(res[1] == 1);
        }

        [Fact]
        public void ToDictionary6()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            AssertThrows<Exception>(() => xs.ToDictionary(x => x % 2, new Eq()).Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is ArgumentException);
        }

        [Fact]
        public void ToDictionary7()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = xs.ToDictionary(x => x % 2, x => x, new Eq()).Result;
            Assert.True(res[0] == 4);
            Assert.True(res[1] == 1);
        }

        [Fact]
        public async Task ToLookup_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(default(IAsyncEnumerable<int>), x => 0));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(AsyncEnumerable.Return(42), default(Func<int, int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(default(IAsyncEnumerable<int>), x => 0, EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(AsyncEnumerable.Return(42), x => 0, default(IEqualityComparer<int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(default(IAsyncEnumerable<int>), x => 0, x => 0));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), x => 0));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), x => 0, default(Func<int, int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(default(IAsyncEnumerable<int>), x => 0, x => 0, EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), x => 0, EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), x => 0, default(Func<int, int>), EqualityComparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), x => 0, x => 0, default(IEqualityComparer<int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(default(IAsyncEnumerable<int>), x => 0, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), CancellationToken.None));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(default(IAsyncEnumerable<int>), x => 0, EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int>(AsyncEnumerable.Return(42), x => 0, default(IEqualityComparer<int>), CancellationToken.None));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(default(IAsyncEnumerable<int>), x => 0, x => 0, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), x => 0, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), x => 0, default(Func<int, int>), CancellationToken.None));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(default(IAsyncEnumerable<int>), x => 0, x => 0, EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), default(Func<int, int>), x => 0, EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), x => 0, default(Func<int, int>), EqualityComparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToLookup<int, int, int>(AsyncEnumerable.Return(42), x => 0, x => 0, default(IEqualityComparer<int>), CancellationToken.None));
        }

        [Fact]
        public void ToLookup1()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = xs.ToLookup(x => x % 2).Result;
            Assert.True(res.Contains(0));
            Assert.True(res.Contains(1));
            Assert.True(res[0].Contains(4));
            Assert.True(res[1].Contains(1));
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void ToLookup2()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            var res = xs.ToLookup(x => x % 2).Result;
            Assert.True(res.Contains(0));
            Assert.True(res.Contains(1));
            Assert.True(res[0].Contains(4));
            Assert.True(res[0].Contains(2));
            Assert.True(res[1].Contains(1));
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void ToLookup3()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = xs.ToLookup(x => x % 2, x => x + 1).Result;
            Assert.True(res.Contains(0));
            Assert.True(res.Contains(1));
            Assert.True(res[0].Contains(5));
            Assert.True(res[1].Contains(2));
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void ToLookup4()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            var res = xs.ToLookup(x => x % 2, x => x + 1).Result;
            Assert.True(res.Contains(0));
            Assert.True(res.Contains(1));
            Assert.True(res[0].Contains(5));
            Assert.True(res[0].Contains(3));
            Assert.True(res[1].Contains(2));
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void ToLookup5()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = xs.ToLookup(x => x % 2, new Eq()).Result;
            Assert.True(res.Contains(0));
            Assert.True(res.Contains(1));
            Assert.True(res[0].Contains(4));
            Assert.True(res[1].Contains(1));
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void ToLookup6()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            var res = xs.ToLookup(x => x % 2, new Eq()).Result;
            Assert.True(res.Contains(0));
            Assert.True(res.Contains(1));
            Assert.True(res[0].Contains(4));
            Assert.True(res[0].Contains(2));
            Assert.True(res[1].Contains(1));
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void ToLookup7()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            var res = xs.ToLookup(x => x % 2).Result;
            foreach (var g in res)
                Assert.True(g.Key == 0 || g.Key == 1);
        }

        [Fact]
        public void ToLookup8()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            var res = xs.ToLookup(x => x % 2).Result;
#pragma warning disable IDE0007 // Use implicit type
            foreach (IGrouping<int, int> g in (IEnumerable)res)
                Assert.True(g.Key == 0 || g.Key == 1);
#pragma warning restore IDE0007 // Use implicit type
        }

        [Fact]
        public void ToLookup9()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            var res = xs.ToLookup(x => x % 2, x => x, new Eq()).Result;
            Assert.True(res.Contains(0));
            Assert.True(res.Contains(1));
            Assert.True(res[0].Contains(4));
            Assert.True(res[0].Contains(2));
            Assert.True(res[1].Contains(1));
            Assert.True(res.Count == 2);
        }

        [Fact]
        public async Task Min_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.Min(default(IAsyncEnumerable<DateTime>), Comparer<DateTime>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.Min(AsyncEnumerable.Empty<DateTime>(), default(IComparer<DateTime>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.Min(default(IAsyncEnumerable<DateTime>), Comparer<DateTime>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.Min(AsyncEnumerable.Empty<DateTime>(), default(IComparer<DateTime>), CancellationToken.None));
        }

        [Fact]
        public async Task Max_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.Max(default(IAsyncEnumerable<DateTime>), Comparer<DateTime>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.Max(AsyncEnumerable.Empty<DateTime>(), default(IComparer<DateTime>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.Max(default(IAsyncEnumerable<DateTime>), Comparer<DateTime>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.Max(AsyncEnumerable.Empty<DateTime>(), default(IComparer<DateTime>), CancellationToken.None));
        }

        [Fact]
        public async Task MinBy_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(default(IAsyncEnumerable<int>), x => x));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(AsyncEnumerable.Return(42), default(Func<int, int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(default(IAsyncEnumerable<int>), x => x, Comparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(AsyncEnumerable.Return(42), default(Func<int, int>), Comparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(AsyncEnumerable.Return(42), x => x, default(IComparer<int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(default(IAsyncEnumerable<int>), x => x, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(AsyncEnumerable.Return(42), default(Func<int, int>), CancellationToken.None));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(default(IAsyncEnumerable<int>), x => x, Comparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(AsyncEnumerable.Return(42), default(Func<int, int>), Comparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MinBy(AsyncEnumerable.Return(42), x => x, default(IComparer<int>), CancellationToken.None));
        }

        [Fact]
        public void MinBy1()
        {
            var xs = new[] { 3, 5, 7, 6, 4, 2 }.ToAsyncEnumerable().MinBy(x => x / 2);
            var res = xs.Result;

            Assert.True(res.SequenceEqual(new[] { 3, 2 }));
        }

        [Fact]
        public void MinBy2()
        {
            var xs = new int[0].ToAsyncEnumerable().MinBy(x => x / 2);

            AssertThrows<Exception>(() => xs.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void MinBy3()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 3, 5, 7, 6, 4, 2 }.ToAsyncEnumerable().MinBy(x => { if (x == 3) throw ex; return x; });

            AssertThrows<Exception>(() => xs.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void MinBy4()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 3, 5, 7, 6, 4, 2 }.ToAsyncEnumerable().MinBy(x => { if (x == 4) throw ex; return x; });

            AssertThrows<Exception>(() => xs.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void MinBy5()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 3, 5, 7, 6, 4, 2 }.ToAsyncEnumerable().Concat(AsyncEnumerable.Throw<int>(ex)).MinBy(x => x, Comparer<int>.Default);

            AssertThrows<Exception>(() => xs.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public async Task MaxBy_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(default(IAsyncEnumerable<int>), x => x));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(AsyncEnumerable.Return(42), default(Func<int, int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(default(IAsyncEnumerable<int>), x => x, Comparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(AsyncEnumerable.Return(42), default(Func<int, int>), Comparer<int>.Default));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(AsyncEnumerable.Return(42), x => x, default(IComparer<int>)));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(default(IAsyncEnumerable<int>), x => x, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(AsyncEnumerable.Return(42), default(Func<int, int>), CancellationToken.None));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(default(IAsyncEnumerable<int>), x => x, Comparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(AsyncEnumerable.Return(42), default(Func<int, int>), Comparer<int>.Default, CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerableEx.MaxBy(AsyncEnumerable.Return(42), x => x, default(IComparer<int>), CancellationToken.None));
        }

        [Fact]
        public void MaxBy1()
        {
            var xs = new[] { 3, 5, 7, 6, 4, 2 }.ToAsyncEnumerable().MaxBy(x => x / 2);
            var res = xs.Result;

            Assert.True(res.SequenceEqual(new[] { 7, 6 }));
        }

        [Fact]
        public void MaxBy2()
        {
            var xs = new int[0].ToAsyncEnumerable().MaxBy(x => x / 2);

            AssertThrows<Exception>(() => xs.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is InvalidOperationException);
        }

        [Fact]
        public void MaxBy3()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 3, 5, 7, 6, 4, 2 }.ToAsyncEnumerable().MaxBy(x => { if (x == 3) throw ex; return x; });

            AssertThrows<Exception>(() => xs.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void MaxBy4()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 3, 5, 7, 6, 4, 2 }.ToAsyncEnumerable().MaxBy(x => { if (x == 4) throw ex; return x; });

            AssertThrows<Exception>(() => xs.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void MaxBy5()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 3, 5, 7, 6, 4, 2 }.ToAsyncEnumerable().Concat(AsyncEnumerable.Throw<int>(ex)).MaxBy(x => x, Comparer<int>.Default);

            AssertThrows<Exception>(() => xs.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        private sealed class Eq : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return EqualityComparer<int>.Default.Equals(Math.Abs(x), Math.Abs(y));
            }

            public int GetHashCode(int obj)
            {
                return EqualityComparer<int>.Default.GetHashCode(Math.Abs(obj));
            }
        }
    }
}
