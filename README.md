操作QQ的聊天窗口
==================
####1.实现读取QQ聊天窗口内的消息记录（QQ聊天窗口的上半部分）。该部分工作稳定不会出现问题。<br>
####2.实现向QQ消息输入框输入消息，然后发送出去。目前该部分基本可以工作。：<br>
* 2.1 腾讯没有开放向输入框输入消息的接口，所以不能直接输入到消息输入框,UI自动化接口是没有办法的。<br>
* 2.2 目前用sendmessage向窗口发送消息。虽然消息可以发送出去，但是存在一个问题：当有人操作了聊天窗口，将光标移动到了输入框外，程序就不能将消息输入到对话框内部了，也就不能发送消息了。好在默认情况下当聊天窗口打开或者还原的时候光标会自动定位到输入框内。<br>
* 2.3 为防止2.2的问题，通过SetWindowPos将窗口移动到显示器之外来实现隐藏聊天窗口的目的（不能用ShowWindow隐藏，隐藏后就找不到窗口了）。<br>
使用
========
1.sample有使用方法，非常简单。<br>
创建一个QQChatWindow类的实例a,读取消息就是a.readQQMessage("QQ聊天窗口名")，注意QQ聊天窗口不能合并，要单独的经典窗口。 a.sendQQMessage("窗口名", "消息内容")就可以发送消息。<br>
2.功能简陋，仅仅适用于软件报错提醒，比如软件运行出错以后要及时的提醒人员去维护；群消息分析，从群消息中筛选关键词或者对自己有用的信息等。<br>
###3.非法使用概不负责，使用者承担所有法律责任。<br>