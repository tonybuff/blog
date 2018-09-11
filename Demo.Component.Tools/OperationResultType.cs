using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Component.Tools
{
    [Description("业务操作结果的枚举")]
    public enum OperationResultType
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        Success,

        /// <summary>
        /// 操作未引发改变，提交取消
        /// </summary>
        [Description("操作没有引发改变，提交被取消")]
        NoChanged,

        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        ParamError,

        /// <summary>
        /// 指定的参数的查询不存在
        /// </summary>
        [Description("查询数据不存在")]
        QueryNull,

        /// <summary>
        /// 权限不足
        /// </summary>
        [Description("当前用户权限不足，不能继续操作。")]
        PurviewLack,

        /// <summary>
        /// 非法操作
        /// </summary>
        [Description("非法操作。")]
        IllegalOperation,

        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Warning,

        /// <summary>
        ///操作引发错误
        /// </summary>
        [Description("操作引发错误。")]
        Error,
    }
}
