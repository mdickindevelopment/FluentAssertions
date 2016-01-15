using System;
using System.Collections.Generic;

namespace FluentAssertions
{
	public interface IAssertChain<T>
	{
		T Item { get; }
		IAssertChain<T> AreEqual(object expected, Func<T, object> actualFunc, string message = null, params object[] messageParameters);
		IAssertChain<T> AreNotEqual(object expected, Func<T, object> actualFunc, string message = null, params object[] messageParameters);
		IAssertChain<T> IsTrue(Func<T, bool> actualFunc, string message = null, params object[] messageParameters);
		IAssertChain<T> IsFalse(Func<T, bool> actualFunc, string message = null, params object[] messageParameters);
		IAssertChain<T> IsNull(Func<T, object> actualFunc, string message = null, params object[] messageParameters);
		IAssertChain<T> IsNotNull(Func<T, object> actualFunc, string message = null, params object[] messageParameters);
		IAssertChain<T> With<TNew>(Func<T, TNew> childFunc, Action<IAssertChain<TNew>> assertChain);
		IAssertChain<T> With<TNew>(Func<T, IEnumerable<TNew>> childCollectionFunc, int index,
			Action<IAssertChain<TNew>> assertChain);
		IAssertChain<T> With<TNew>(Func<T, IEnumerable<TNew>> childCollectionFunc,
			Action<ICollectionAssertChain<TNew>> assertChain);

	}

	public interface ICollectionAssertChain<T>
	{
		IEnumerable<T> Collection { get; }
		ICollectionAssertChain<T> AreEqual(object expected, Func<IEnumerable<T>, object> actualFunc, string message = null, params object[] messageParameters);
		ICollectionAssertChain<T> IsTrue(Func<IEnumerable<T>, bool> actualFunc, string message = null, params object[] messageParameters);
		ICollectionAssertChain<T> IsFalse(Func<IEnumerable<T>, bool> actualFunc, string message = null, params object[] messageParameters);
		ICollectionAssertChain<T> HasCount(int count, string message = null, params object[] messageParameters);
		ICollectionAssertChain<T> WithItem(int index, Action<IAssertChain<T>> assertChain);
	}
}