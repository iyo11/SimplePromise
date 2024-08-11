using System;
using System.Collections.Generic;

namespace SimplePromise
{
    public class Promise<T> : IPromise<T>
    {
        private PromiseState _state = PromiseState.Pending;
        private T _result;
        private Exception _exception;
        private readonly List<PromiseHandler<T>> _handlers = new List<PromiseHandler<T>>();

        public PromiseState State => _state;

        public Promise(Action<Action<T>, Action<Exception>> callback)
        {
            try
            {
                callback(Resolve, Reject);
            }
            catch (Exception exception)
            {
                Reject(exception);
            }
        }

        private void Resolve(T result)
        {
            if (_state != PromiseState.Pending) return;
            _state = PromiseState.Resolved;
            _result = result;
            Execute();
        }

        private void Reject(Exception exception)
        {
            if (_state != PromiseState.Pending) return;
            _state = PromiseState.Rejected;
            _exception = exception;
            Execute();
        }

        public Promise<T> Then(Action<T> onFullFilled, Action<Exception> onRejected)
        {
            _handlers.Add(new PromiseHandler<T>
            {
                OnFullFilled = onFullFilled,
                OnRejected = onRejected,
            });
            if (_state != PromiseState.Pending)
            {
                Execute();
            }
            return this;
        }

        private void Execute()
        {
            if (_state == PromiseState.Pending) return;
            foreach (var handler in _handlers)
            {
                switch (_state)
                {
                    case PromiseState.Resolved:
                        handler.OnFullFilled(_result);
                        break;
                    case PromiseState.Rejected:
                        handler.OnRejected(_exception);
                        break;
                    case PromiseState.Pending:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // 清空处理器列表以避免重复执行。
            _handlers.Clear();
        }
        
        public Promise<T> Catch(Action<Exception> onRejected)
        {
            throw new NotImplementedException();
        }

        public Promise<T> Finally(Action<T> onFullFilled, Action<Exception> onRejected)
        {
            throw new NotImplementedException();
        }

        public Promise<T> Finally(Action<T> onFullFilled)
        {
            throw new NotImplementedException();
        }

        public Promise<T> Finally(Action<Exception> onRejected)
        {
            throw new NotImplementedException();
        }
    }
}