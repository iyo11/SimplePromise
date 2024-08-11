using System;

namespace SimplePromise
{
    public enum PromiseState
    {
        Pending,
        Resolved,
        Rejected
    }

    public class PromiseHandler<T>
    {
        public Action<T> OnFullFilled;
        
        public Action<Exception> OnRejected;
        
        public Action<T> Resolve;
        
        public Action<Exception> Reject;
    }

    internal interface IPromise<T>
    {
        PromiseState State { get; }
        
        //void Resolve(T value);
        //void Reject(Exception exception);
        
        
        Promise<T> Then(Action<T> onFullFilled, Action<Exception> onRejected);
        
        Promise<T> Catch(Action<Exception> onRejected);
        
        Promise<T> Finally(Action<T> onFullFilled, Action<Exception> onRejected);
        
        Promise<T> Finally(Action<T> onFullFilled);
        
        Promise<T> Finally(Action<Exception> onRejected);
    }
}
