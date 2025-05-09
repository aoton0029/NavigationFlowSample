using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationFlowSample.Core
{
    /// <summary>
    /// 簡易的なServiceProvider実装
    /// </summary>
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, ServiceRegistration> _services = new();

        /// <summary>
        /// 指定された型のサービスを取得します
        /// </summary>
        /// <param name="serviceType">取得するサービスの型</param>
        /// <returns>サービスのインスタンス、または登録されていない場合はnull</returns>
        public object GetService(Type serviceType)
        {
            if (_services.TryGetValue(serviceType, out var registration))
            {
                return registration.GetInstance(this);
            }
            return null;
        }

        /// <summary>
        /// 指定された型のサービスを取得します
        /// </summary>
        /// <typeparam name="T">取得するサービスの型</typeparam>
        /// <returns>サービスのインスタンス</returns>
        public T GetService<T>() where T : class
        {
            return GetService(typeof(T)) as T;
        }

        /// <summary>
        /// トランジェントサービスを登録します（毎回新しいインスタンスが生成されます）
        /// </summary>
        /// <typeparam name="TService">サービスのインターフェース型</typeparam>
        /// <typeparam name="TImplementation">実装の型</typeparam>
        public void RegisterTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _services[typeof(TService)] = new TransientRegistration(typeof(TImplementation));
        }

        /// <summary>
        /// トランジェントサービスをインスタンスファクトリで登録します
        /// </summary>
        /// <typeparam name="TService">サービスの型</typeparam>
        /// <param name="factory">インスタンス生成用のファクトリ関数</param>
        public void RegisterTransient<TService>(Func<IServiceProvider, TService> factory)
            where TService : class
        {
            _services[typeof(TService)] = new FactoryRegistration<TService>(factory);
        }

        /// <summary>
        /// シングルトンサービスを登録します（一度だけインスタンス化されます）
        /// </summary>
        /// <typeparam name="TService">サービスのインターフェース型</typeparam>
        /// <typeparam name="TImplementation">実装の型</typeparam>
        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _services[typeof(TService)] = new SingletonRegistration(typeof(TImplementation));
        }

        /// <summary>
        /// シングルトンサービスをインスタンスで登録します
        /// </summary>
        /// <typeparam name="TService">サービスの型</typeparam>
        /// <param name="instance">サービスのインスタンス</param>
        public void RegisterSingleton<TService>(TService instance)
            where TService : class
        {
            _services[typeof(TService)] = new InstanceRegistration(instance);
        }

        // サービス登録の抽象基底クラス
        private abstract class ServiceRegistration
        {
            public abstract object GetInstance(IServiceProvider provider);
        }

        // トランジェント（毎回新しいインスタンス）登録
        private class TransientRegistration : ServiceRegistration
        {
            private readonly Type _implementationType;

            public TransientRegistration(Type implementationType)
            {
                _implementationType = implementationType;
            }

            public override object GetInstance(IServiceProvider provider)
            {
                return Activator.CreateInstance(_implementationType);
            }
        }

        // シングルトン（単一インスタンス）登録
        private class SingletonRegistration : ServiceRegistration
        {
            private readonly Type _implementationType;
            private object _instance;

            public SingletonRegistration(Type implementationType)
            {
                _implementationType = implementationType;
            }

            public override object GetInstance(IServiceProvider provider)
            {
                if (_instance == null)
                {
                    _instance = Activator.CreateInstance(_implementationType);
                }
                return _instance;
            }
        }

        // インスタンス登録
        private class InstanceRegistration : ServiceRegistration
        {
            private readonly object _instance;

            public InstanceRegistration(object instance)
            {
                _instance = instance;
            }

            public override object GetInstance(IServiceProvider provider)
            {
                return _instance;
            }
        }

        // ファクトリ関数による登録
        private class FactoryRegistration<T> : ServiceRegistration where T : class
        {
            private readonly Func<IServiceProvider, T> _factory;

            public FactoryRegistration(Func<IServiceProvider, T> factory)
            {
                _factory = factory;
            }

            public override object GetInstance(IServiceProvider provider)
            {
                return _factory(provider);
            }
        }
    }
}
