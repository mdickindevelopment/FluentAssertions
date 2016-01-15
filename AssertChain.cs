using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentAssertions
{
	public class AssertChain
	{
		private class AssertChainInternal<T> : IAssertChain<T>
		{
			public T Item { get; private set; }

			public AssertChainInternal(T obj)
			{
				Item = obj;
			}

			public IAssertChain<T> AreEqual(object expected, Func<T, object> actualFunc, string message = null, params object[] messageParameters)
			{
				Assert.AreEqual(expected, actualFunc(Item), message, messageParameters);
				return this;
			}

			public IAssertChain<T> AreNotEqual(object expected, Func<T, object> actualFunc, string message = null, params object[] messageParameters)
			{
				Assert.AreNotEqual(expected, actualFunc(Item), message, messageParameters);
				return this;
			}

			public IAssertChain<T> IsTrue(Func<T, bool> actualFunc, string message = null, params object[] messageParameters)
			{
				Assert.IsTrue(actualFunc(Item), message, messageParameters);
				return this;
			}

			public IAssertChain<T> IsFalse(Func<T, bool> actualFunc, string message = null, params object[] messageParameters)
			{
				Assert.IsFalse(actualFunc(Item), message, messageParameters);
				return this;
			}

			public IAssertChain<T> IsNull(Func<T, object> actualFunc, string message = null, params object[] messageParameters)
			{
				Assert.IsNull(actualFunc(Item), message, messageParameters);
				return this;
			}

			public IAssertChain<T> IsNotNull(Func<T, object> actualFunc, string message = null, params object[] messageParameters)
			{
				Assert.IsNotNull(actualFunc(Item), message, messageParameters);
				return this;
			}

			public IAssertChain<T> With<TNew>(Func<T, TNew> childFunc, Action<IAssertChain<TNew>> assertChain)
			{
				var chain = new AssertChainInternal<TNew>(childFunc(Item));
				assertChain(chain);
				return this;
			}

			public IAssertChain<T> With<TNew>(Func<T, IEnumerable<TNew>> childCollectionFunc, int index, Action<IAssertChain<TNew>> assertChain)
			{
				return With(m => childCollectionFunc(Item).ElementAt(index), assertChain);
			}

			public IAssertChain<T> With<TNew>(Func<T, IEnumerable<TNew>> childCollectionFunc, Action<ICollectionAssertChain<TNew>> assertChain)
			{
				var chain = new CollectionAssertChainInternal<TNew>(childCollectionFunc(Item));
				assertChain(chain);
				return this;
			}
		}

		private class CollectionAssertChainInternal<T> : ICollectionAssertChain<T>
		{
			public IEnumerable<T> Collection { get; private set; }

			public CollectionAssertChainInternal(IEnumerable<T> collection)
			{
				Collection = collection;
			}

			public ICollectionAssertChain<T> HasCount(int count, string message = null, params object[] messageParameters)
			{
				Assert.AreEqual(count, Collection.Count(), message, messageParameters);
				return this;
			}

			public ICollectionAssertChain<T> IsTrue(Func<IEnumerable<T>, bool> actualFunc, string message = null, params object[] messageParameters)
			{
				Assert.IsTrue(actualFunc(Collection), message, messageParameters);
				return this;
			}

			public ICollectionAssertChain<T> IsFalse(Func<IEnumerable<T>, bool> actualFunc, string message = null, params object[] messageParameters)
			{
				Assert.IsFalse(actualFunc(Collection), message, messageParameters);
				return this;
			}

			public ICollectionAssertChain<T> AreEqual(object expected, Func<IEnumerable<T>, object> actualFunc, string message = null, params object[] messageParameters)
			{
				Assert.AreEqual(expected, actualFunc(Collection), message, messageParameters);
				return this;
			}

			public ICollectionAssertChain<T> WithItem(int index, Action<IAssertChain<T>> assertChain)
			{
				var item = Collection.ElementAt(index);
				assertChain(new AssertChainInternal<T>(item));
				return this;
			}
		}

		public static ICollectionAssertChain<T> GetFor<T>(IEnumerable<T> objList)
		{
			return new CollectionAssertChainInternal<T>(objList);
		}

		public static ICollectionAssertChain<T> GetFor<T>(List<T> objList)
		{
			return new CollectionAssertChainInternal<T>(objList);
		}

		public static IAssertChain<T> GetFor<T>(T obj)
		{
			return new AssertChainInternal<T>(obj);
		}
	}
}