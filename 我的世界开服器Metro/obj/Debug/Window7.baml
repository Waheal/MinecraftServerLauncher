   M S B A M L   `   `   `  ����U  Q我的世界开服器Metro, Version=5.7.1.2, Culture=neutral, PublicKeyToken=nullG(clr-namespace:我的世界开服器Metro我的世界开服器Metro  5   	   (    "我的世界开服器Metro.Window7   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:我的世界开服器Metro     5   	   -   5          � Title$0  *我的世界开服器——配置服务器��5   	   $	��450��6    $��687.19��6-    � 
ResizeMode$ CanMinimize=�6l    � WindowStartupLocation$ CenterScreen=�6�   ���	#FFFBFBFD.��5	      � .+�5
   
   �� $
��Left=�6   $	��369��6-   $��	10,42,0,0q�6:    �� TextWrapping$
 Wrap=�6M   $����配置文件汉化 
spawn-protection 通过将该值进行 2x+1 的运算来决定出生点的保护半径，设置为0将只保护出生点下方那一个方块。 
max-tick-time 设置每个tick花费的最大毫秒数 
query.port 设置监听服务器的端口号 
generator-settings 用于自定义超平坦世界的生成 
sync-chunk-writes 为true时区块文件以同步模式写入(跟本地一样加载) 
force-gamemode 强制玩家加入时为默认游戏模式 
allow-nether 是否允许下界和末地 
enforce-whitelist 在服务器上强制使用白名单 
gamemode 游戏模式 0=生存 1=创造 2=冒险 3=旁观 
broadcast-console-to-ops 向OP广播服务器控制台信息 
enable-query 是否允许使用GameSpy4协议的服务器监听器 
player-idle-timeout 允许的挂机时间，单位为分钟，超过后自动踢出服务器 
text-filtering-config 文本筛选配置（不知道有啥用） 
difficulty 难度0=和平 1=简单 2=普通 3=困难 
spawn-monsters 生成攻击型生物（怪物） 
broadcast-rcon-to-ops 向OP广播RCON信息 
op-permission-level OP权限等级 
pvp 是否允许玩家互相攻击 
entity-broadcast-range-percentage 实体渲染范围百分比 
snooper-enabled 启用数据采集 
level-type 地图的生成类型 
hardcore 极限模式（死后自动封禁） 
enable-status 启用状态 
enable-command-block 启用命令方块 
max-players 服务器最大玩家数限制 
network-compression-threshold 网络压缩阈值 
resource-pack-sha1 资源包的SHA-1值（16进制） 
max-world-size 最大世界大小 
function-permission-level 设定函数的默认权限等级 
rcon.port 设置RCON远程访问的端口号 
server-port 服务器端口 
debug 调试模式 
server-ip 服务器ip，若不绑定请留空 
spawn-npcs 是否生成村民 
allow-flight 是否允许玩家飞行 
level-name 地图名称，不要使用中文 
view-distance 服务器发送给客户端的数据量，决定玩家能设置的视野 
resource-pack 统一资源标识符 (URI) 指向一个资源包。玩家可选择是否使用 
spawn-animals 是否生成动物 
white-list 是否开启白名单 
rcon.password 设置RCON远程访问的密码（参见enable-rcon） 
generate-structures 生成世界时生成结构（如村庄）禁止后地牢和地下要塞仍然生成 
online-mode Minecraft在线（正版）验证 
max-build-height 玩家在服务器放置方块的最高高度 
level-seed 地图种子 
prevent-proxy-connections 是否允许玩家使用网络代理进入服务器 
use-native-transport 是否使用针对Linux平台的数据包收发优化 [ 仅Linux ] 
enable-jmx-monitoring 启用jmx监视 
rate-limit 速率限制 
motd 服务器信息展示 （MOTD） 
enable-rcon 是否允许远程访问服务器控制台（RCON）��6a   $	��Top=�5=   (   $	��363��6@   " �� VerticalScrollBarVisibility$
 Auto=�6L    �� 
IsReadOnly . $��Microsoft YaHei UI)�6�   �� 5>   
   $N��H请参照下边汉化在右边栏更改更改，改好后关闭此窗口��6   $
��Left=�63   $��	10,10,0,0q�6N   $	��Top=�6a   $��16(�6y   �� 5?   
   -   5         ��Name$ item001��5?      $
��Left=�6#   $	��369��6>   $��
373,42,0,0q�6K   $
 Wrap=�6_   $	��Top=�6s   $	��298��6�   $ �� HorizontalScrollBarVisibility$ Visible=�6�   $
 Auto=�6�   5A      5B      