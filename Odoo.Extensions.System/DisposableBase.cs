namespace System
{
    /// <summary>
    /// 可回收的基类
    /// </summary>
#pragma warning disable S3881 // "IDisposable" should be implemented correctly
    public class DisposableBase : IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
    {
        private bool disposed;

        /// <summary>
        /// 是否已回收
        /// </summary>
        protected bool Disposed
        {
            get
            {
#pragma warning disable S2551 // Shared resources should not be used for locking
                lock (this)
#pragma warning restore S2551 // Shared resources should not be used for locking
                {
                    return disposed;
                }
            }
        }

        #region IDisposable Members


        /// <summary>
        /// 回收
        /// </summary>
        public void Dispose()
        {
#pragma warning disable S2551 // Shared resources should not be used for locking
            lock (this)
#pragma warning restore S2551 // Shared resources should not be used for locking
            {
                if (!disposed)
                {
                    Dispose(true);
                    disposed = true;

                    GC.SuppressFinalize(this);
                }
            }
        }

        #endregion

        /// <summary>
        /// 清理
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            //override to provide cleanup
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~DisposableBase()
        {
            Dispose(false);
        }
    }

    /// <summary>
    /// 可回收对象，通过<see cref="Action"/>创建<see cref="IDisposable"/>实例
    /// </summary>
    public class Disposable : DisposableBase
    {
        /// <summary>
        /// 空对象，对象回收时不执行任务动作
        /// </summary>
        public static readonly Disposable Empty = new Disposable();
        /// <summary>
        /// 通过<see cref="Action"/>创建<see cref="IDisposable"/>
        /// </summary>
        /// <param name="cleanup">回收时执行的动作</param>
        /// <returns></returns>
        public static IDisposable Create(Action cleanup)
        {
            return new Disposable { _cleanup = cleanup };
        }
        Action _cleanup;
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
                _cleanup?.Invoke();
        }
    }
}
