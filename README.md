<p></p>
<p></p>
<p align="center">
<img src="https://ftp.bmp.ovh/imgs/2021/06/351eeccfadc07014.png" width = "100" height = "100" alt="图片名称" align=center />
</p>

 <div align="center"> 
  
[![NuGet version (RRQMSkin)](https://img.shields.io/nuget/v/RRQMSkin.svg?style=flat-square)](https://www.nuget.org/packages/RRQMSkin/)
[![License](https://img.shields.io/badge/license-Apache%202-4EB1BA.svg)](https://www.apache.org/licenses/LICENSE-2.0.html)
[![Download](https://img.shields.io/nuget/dt/RRQMSkin)](https://www.nuget.org/packages/RRQMSkin/)
[![star](https://gitee.com/RRQM_OS/RRQMSkin/badge/star.svg?theme=gvp)](https://gitee.com/RRQM_OS/RRQMSkin/stargazers) 
[![fork](https://gitee.com/RRQM_OS/RRQMSkin/badge/fork.svg?theme=gvp)](https://gitee.com/RRQM_OS/RRQMSkin/members)

</div> 

<div align="center">

岂曰无衣？与子同袍。王于兴师，修我戈矛。

</div>
<div align="center">

**简体中文 | [English](./README.md)**

</div>

## 💿描述
&emsp;&emsp;RRQMSkin是由RRQM_OS组织成员开发的一个皮肤库，其中包含多款自定义控件、用户控件、样式资源等。如果大家喜欢请多多支持。

## 🖥支持环境
- .NET Framework4.5及以上。
- .NET Core3.1及以上。

## 🥪支持框架
- WPF

## 🔗联系组织

 - [源代码仓库主页](https://gitee.com/RRQM_OS) 
 - 交流QQ群：234762506

 
## 📦 安装

- [Nuget RRQMSkin](https://www.nuget.org/packages/RRQMSkin/)
- [微软Nuget安装教程](https://docs.microsoft.com/zh-cn/nuget/quickstart/install-and-use-a-package-in-visual-studio)

## 🍻RRQM系产品
| 名称                                           | Nuget版本                                                                                                                              | 下载                                                                                              | 描述                                                                                                                                                                                          |
|------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [RRQMCore](https://gitee.com/RRQM_OS/RRQMCore) | [![NuGet version (RRQMCore)](https://img.shields.io/nuget/v/RRQMCore.svg?style=flat-square)](https://www.nuget.org/packages/RRQMCore/) | [![Download](https://img.shields.io/nuget/dt/RRQMCore)](https://www.nuget.org/packages/RRQMCore/) | RRQMCore是为RRQM系提供基础服务功能的库，其中包含：<br>内存池、对象池、等待逻辑池、AppMessenger、3DES加密、<br>Xml快速存储、运行时间测量器、文件快捷操作、高性能序列化器、<br>规范日志接口等。 |
| [RRQMMVVM](https://gitee.com/RRQM_OS/RRQMMVVM) | [![NuGet version (RRQMMVVM)](https://img.shields.io/nuget/v/RRQMMVVM.svg?style=flat-square)](https://www.nuget.org/packages/RRQMMVVM/) | [![Download](https://img.shields.io/nuget/dt/RRQMMVVM)](https://www.nuget.org/packages/RRQMMVVM/) | RRQMMVVM是超轻简的MVVM框架，但是麻雀虽小，五脏俱全。                                                                                                                                          |
| [RRQMSocket](https://gitee.com/dotnetchina/RRQMSocket) | [![NuGet version (RRQMSocket)](https://img.shields.io/nuget/v/RRQMSocket.svg?style=flat-square)](https://www.nuget.org/packages/RRQMSocket/) | [![Download](https://img.shields.io/nuget/dt/RRQMSocket)](https://www.nuget.org/packages/RRQMSocket/) | RRQMSocket是一个整合性网络通信框架，特点是支持高并发、事件驱动、易用性强、二次开发难度低等。其中主要内容包括:TCP服务通信框架、文件传输、RPC等内容|  

## 一、窗体

#### 1.1 增强性普通窗体（RRQMWindow）
 **说明：** 

增强性普通窗体是在**无边框窗体**的基础之上实现的自定义Icon（绿色区）、TitleContent（红色区）、ToolCommand（蓝色区）、Content（紫色区）。

 **特点：** 
1. 窗体响应鼠标全指令，包括拖动最大化、最小化、左右分屏、改变尺寸等。
2. TitleContent区内容可填充object。
3. 最小化、最大化切换、关闭均采用Command绑定形式，可以很自由的实现个性化功能。
4. 可重写母模板。

![输入图片说明](https://images.gitee.com/uploads/images/2021/0429/090158_405db863_8553710.png "屏幕截图.png")

#### 1.2 增强性效果窗体（RRQMEffectWindow）
 **说明：** 

增强性效果窗体是在继承自**RRQMWindow**的可自定义实现特效的窗体，其功能区布局和RRQMWindow一样。

 **特点：** 
1. 窗体响应鼠标全指令，包括拖动最大化、最小化、左右分屏、改变尺寸等。
2. TitleContent区内容可填充object。
3. 最小化、最大化切换、关闭均采用Command绑定形式，可以很自由的实现个性化功能。
4. 可重写母模板。
5. 使用属性直接设置 **窗体圆角** 、 **阴影扩散度** 、 **阴影颜色** 等。

![输入图片说明](https://images.gitee.com/uploads/images/2021/0429/091244_b0abd404_8553710.png "屏幕截图.png")
## 二、元素


## 三、自定义控件
## 四、控件样式资源

## 致谢

谢谢大家对我的支持，如果还有其他问题，请加群QQ：234762506讨论。


## 💕 支持本项目（为组织）
您的支持就是我们不懈努力的动力。打赏时请一定留下您的称呼，以及RRQMSkin注释。

 **赞助总金额:0￥** 

**赞助名单：** 

（以下排名只按照打赏时间顺序）


<img src="https://images.gitee.com/uploads/images/2021/0330/234046_7662fb8c_8553710.png" width = "600" height = "400" alt="图片名称" align=center />

