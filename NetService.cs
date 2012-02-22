﻿using System;
using System.Net.Sockets;
using NewLife.Model;
using NewLife.Net.Common;
using NewLife.Net.Proxy;
using NewLife.Net.Sockets;
using NewLife.Net.Tcp;
using NewLife.Net.Udp;
using NewLife.Reflection;

namespace NewLife.Net
{
    /// <summary>网络服务对象提供者</summary>
    public class NetService : ServiceContainer<NetService>
    {
        static NetService()
        {
            IObjectContainer container = Container;
            container.Register<IProxySession, ProxySession>()
                .Register<ISocketServer, TcpServer>(ProtocolType.Tcp)
                .Register<ISocketServer, UdpServer>(ProtocolType.Udp)
                .Register<ISocketClient, TcpClientX>(ProtocolType.Tcp)
                .Register<ISocketClient, UdpClientX>(ProtocolType.Udp)
                .Register<ISocketSession, TcpClientX>(ProtocolType.Tcp)
                .Register<ISocketSession, UdpServer>(ProtocolType.Udp)
                .Register<IStatistics, Statistics>()
                .Register<INetSession, NetSession>();
        }

        /// <summary>安装，引发静态构造函数</summary>
        public static void Install() { }

        #region 方法
        /// <summary>解析符合条件的类型</summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Type ResolveType<TInterface>(Func<IObjectMap, Boolean> func)
        {
            foreach (IObjectMap item in Container.ResolveAllMaps(typeof(TInterface)))
            {
                if (func(item)) return item.ImplementType;
            }

            return null;
        }

        //public static T Resolve<T>(ProtocolType protocol) where T : ISocket
        //{
        //    return Resolve<T>(protocol);
        //}
        #endregion
    }
}