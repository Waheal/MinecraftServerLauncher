   M S B A M L   `   `   `  ����U  Q我的世界开服器Metro, Version=5.7.1.2, Culture=neutral, PublicKeyToken=nullG(clr-namespace:我的世界开服器Metro我的世界开服器Metro  5   	   (    "我的世界开服器Metro.Window4   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:我的世界开服器Metro     5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5   	   -   5          � Title$0  *我的世界开服器——下载服务端��5	   	   $	��450��6    $	��538��6-    � 
ResizeMode$ NoResize=�6P    � WindowStartupLocation$ CenterScreen=�6f   ���	#FFFBFBFD.��5
      � .+�5   
   -  'MahApps.Metro.Controls.MetroProgressBar  -   5          Name$ downprog��5      $
��Left=�61   $��38��6L   $��	0,373,0,0q�6X   $	��Top=�6k   $	��522��6�   ���	#FF4AAEFF6�   �� 5   
   -   5         ��Name$ serverlist1��5      $
��Left=�6'   $	��331��6B   $��	10,37,0,0q�6O   $	��Top=�6b   $	��309��6z   ��6�   Z� �� 5   
   -   5        $ serverdownlist��5      $
��Left=�6*   $	��100��6E   $��
65,271,0,0q�6R   $	��Top=�6f   $	��100��6~   $y�Hidden=�6�   �� 5   
   -   5         ��Name$ getservermsg��5      $
��Left=�6*   $��111,151,0,0q�6E   $
��Wrap=�6Z   $0��*加载中，请勿随意移动该窗口...��6n   $	��Top=�6�   $��39��6�   $	��169��6�   �� 5   
   -   5         ��Name$ downmsg1��5      $y��s下载进度（默认下载目录为软件运行目录的ServerLauncher文件夹），下载服务由 xiaoyu 提供��6"   $
��Left=�6b   $��	7,380,0,0q�6}   $	��Top=�6�   $��26��6�   �� 5   
   -   5         ��Name$ updatehistory��5      $��服务端更新日志��6(   $
��Left=�6:   $��	355,6,0,0q�6U   $	��Top=�6h   $	��144��6�   $��Microsoft YaHei UI)�6�   $��12(�6�   $��42��6�   $��Normal"�6�   ��6�   Z� 6�   )  #MahApps.Metro.Controls.ProgressRing  5   
   -   5          Name$ 	lodingIco��5      $
��Left=�6.   $��
65,139,0,0q�6I   $	��Top=�6]   $��41��6u   $��41��6�   �� 5   
   -	   5        	 ��Name$	 
serverlist��5      $
��Left=�6'   $��10,6,0,0q�6B   $	��Top=�6T   $	��309��6l   $��Microsoft YaHei UI)�6�   ��6�   Z� �� 5   
   $
��Left=�6   $	��121��6-   $��324,247,0,0q�6:   
 �� TextWrapping$

 Wrap=�6O   $����我该如何选择服务端？
Spigot——只可加插件
Paper——只可加插件
CraftBukkit——只可加插件
Mojang——纯原版，啥也不能加
其他MOD端——均可加MOD，有的可加插件��6c   $	��Top=�6�   $	��198��6�   |�.   �� 
IsReadOnly . $��Microsoft YaHei UI)�6'  ��6G  Z� �� 5   
   -
   5        $ 
openSpigot��5      $��SpigotMc��6%   $
��Left=�68   $��
355,93,0,0q�6S   $	��Top=�6g   $	��144��6   $��Microsoft YaHei UI)�6�   $��12(�6�   $��42��6�   $��Normal"�6�   ��6�   Z� 6�   �� 5   
   -   5        $ 	openPaper��5      $��PaperMc��6$   $
��Left=�66   $��355,140,0,0q�6Q   $	��Top=�6f   $	��144��6~   $��Microsoft YaHei UI)�6�   $��12(�6�   $��42��6�   $��Normal"�6�   ��6�   Z� 6�   �� 5   
   -   5        $ 
openMojang��5      $��Mojang��6%   $
��Left=�66   $��355,187,0,0q�6Q   $	��Top=�6f   $	��144��6~   $��Microsoft YaHei UI)�6�   $��12(�6�   $��42��6�   $��Normal"�6�   ��6�   Z� 6�   �� 5   
   $��相关网站��6   $
��Left=�6   $��
324,63,0,0q�6:   $	��Top=�6N   5      5      