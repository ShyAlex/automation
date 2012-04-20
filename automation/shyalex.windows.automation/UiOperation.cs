using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ShyAlex.Windows.Automation
{
    public class UiOperation<T>
    {
        private const Int32 OPERATION_TIMEOUT = 5000;

        private const Int32 OPERATION_RETRY_DELAY = 150;

        private readonly Int32 operationTimeout;

        private readonly Int32 retryDelay;

        private readonly T initialValue;

        private readonly Func<T> getValue;

        private readonly Func<T, Boolean> successCondition;

        public UiOperation(Func<T> getValue)
            : this(default(T), getValue, v => v != null && !v.Equals(default(T))) { }

        public UiOperation(T initialValue, Func<T> getValue, Func<T, Boolean> successCondition)
            : this(initialValue, getValue, successCondition, OPERATION_TIMEOUT, OPERATION_RETRY_DELAY) { }

        public UiOperation(T initialValue, Func<T> getValue, Func<T, Boolean> successCondition, Int32 operationTimeout, Int32 retryDelay)
        {
            if (getValue == null)
            {
                throw new ArgumentNullException("getValue");
            }
            if (successCondition == null)
            {
                throw new ArgumentNullException("successCondition");
            }

            this.initialValue = initialValue;
            this.getValue = getValue;
            this.successCondition = successCondition;
            this.operationTimeout = operationTimeout;
            this.retryDelay = retryDelay;
        }

        public T Invoke()
        {
            var value = initialValue;
            var started = DateTime.Now;

            do
            {
                value = getValue();

                if (successCondition(value))
                {
                    break;
                }

                Thread.Sleep(retryDelay);
            } while ((DateTime.Now - started).TotalMilliseconds <= operationTimeout);

            return value;
        }
    }
}
