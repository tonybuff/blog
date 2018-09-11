using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Component.Tools
{
    /// <summary>
    /// 业务操作结果信息类，对操作结果进行封装
    /// </summary>
    public class OperationResult
    {
        #region 属性
        /// <summary>
        /// 获取或设置操作返回结果类型
        /// </summary>
        public OperationResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置操作返回附加内容
        /// </summary>
        public object AppendData { get; set; }

        /// <summary>
        /// 获取或设置操作返回的，用户记录日志。日志信息
        /// </summary>
        public string LogMessage { get; set; } 
        #endregion

        #region 方法
        /// <summary>
        /// 初始化业务操作结果信息类的实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        public OperationResult(OperationResultType resultType)
        {
            ResultType = resultType;
        }

        /// <summary>
        /// 初始化定义返回消息的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        public OperationResult(OperationResultType resultType, string message)
            : this(resultType)
        {
            Message = message;
        }

        /// <summary>
        ///  初始化一个 定义返回消息与日志消息的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType"></param>
        /// <param name="message"></param>
        /// <param name="logMesage"></param>
        public OperationResult(OperationResultType resultType, string message, string logMesage)
            : this(resultType, message)
        {
            LogMessage = logMesage;
        }

        /// <summary>
        ///初始化一个 定义返回消息、日志消息与附加数据的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType"></param>
        /// <param name="message"></param>
        /// <param name="logMessage"></param>
        /// <param name="appendData"></param>
        public OperationResult(OperationResultType resultType, string message, string logMessage, object appendData)
            : this(resultType, message, logMessage)
        {
            AppendData = appendData;
        }

        /// <summary>
        ///初始化一个 定义返回消息与附加数据的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        /// <param name="appendData">业务返回数据</param>
        public OperationResult(OperationResultType resultType, string message, object appendData)
            : this(resultType, message)
        {
            AppendData = appendData;
        }
        
        #endregion

    }
}
