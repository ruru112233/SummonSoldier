;--------------------------------------------------------------------------------
; ティラノスクリプト テーマプラグイン theme_kopanda_bth_02
;--------------------------------------------------------------------------------

[iscript]

mp.font_color    = mp.font_color    || "0xEEEEEE"; // テキストのフォントカラー
mp.name_color    = mp.name_color    || "0x333333"; // 名前のフォントカラー
mp.frame_opacity = mp.frame_opacity || "255"; // メッセージウィンドウの不透明度
mp.font_color2   = mp.font_color2   || "0xEEEEEE";
mp.glyph         = mp.glyph         || "off";

// Config.tjsで既読テキストのフォントカラーを「default」にしている場合はmp.font_color2は反映されません
if(TG.config.alreadyReadTextColor != "default"){
	TG.config.alreadyReadTextColor = mp.font_color2;
}

[endscript]

;名前部分のメッセージレイヤ削除
[free name="chara_name_area" layer="message0"]

;メッセージウィンドウの設定
[position layer="message0" width="1280" height="224" top="486" left="0"]
[position layer="message0" frame="../others/plugin/theme_kopanda_bth_02/image/frame_message.png" margint="80" marginl="160" marginr="150" opacity="&mp.frame_opacity" page="fore"]

;名前枠の設定
[ptext name="chara_name_area" layer="message0" color="&mp.name_color" bold="bold" size="28" x="50" y="505" width="420" align="center"]
[chara_config ptext="chara_name_area"]

;デフォルトのフォントカラー指定
[font color="&mp.font_color"]
[deffont color="&mp.font_color"]

; クリック待ちグリフの設定（on設定時のみ有効）※ティラノスクリプトv507以降
[if exp="mp.glyph == 'on'"]
[glyph line="../../../data/others/plugin/theme_kopanda_bth_02/image/system/nextpage.png"]
[endif]

;=================================================================================

; 機能ボタンを表示するマクロ

;=================================================================================

; 機能ボタンを表示したいシーンで[add_theme_button]と記述してください（消去は[clearfix]タグ）

[macro name="add_theme_button"]

; 歯車ボタン（メニューボタン）非表示
[hidemenubutton]

; クイックセーブボタン
[button name="role_button" role="quicksave" graphic="../others/plugin/theme_kopanda_bth_02/image/button/qsave.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/qsave2.png" x="290" y="686"]

; クイックロードボタン
[button name="role_button" role="quickload" graphic="../others/plugin/theme_kopanda_bth_02/image/button/qload.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/qload2.png" x="385" y="686"]

; セーブボタン
[button name="role_button" role="save" graphic="../others/plugin/theme_kopanda_bth_02/image/button/save.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/save2.png" x="480" y="686"]

; ロードボタン
[button name="role_button" role="load" graphic="../others/plugin/theme_kopanda_bth_02/image/button/load.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/load2.png" x="550" y="686"]

; オートボタン
[button name="role_button" role="auto" graphic="../others/plugin/theme_kopanda_bth_02/image/button/auto.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/auto2.png" x="620" y="686"]

; スキップボタン
[button name="role_button" role="skip" graphic="../others/plugin/theme_kopanda_bth_02/image/button/skip.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/skip2.png" x="690" y="686"]

; バックログボタン
[button name="role_button" role="backlog" graphic="../others/plugin/theme_kopanda_bth_02/image/button/log.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/log2.png" x="755" y="686"]

; フルスクリーン切替ボタン
[button name="role_button" role="fullscreen" graphic="../others/plugin/theme_kopanda_bth_02/image/button/screen.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/screen2.png" x="830" y="686"]

; メニュー呼び出しボタン
[button name="role_button" role="menu" graphic="../others/plugin/theme_kopanda_bth_02/image/button/menu.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/menu2.png" x="920" y="686"]

; コンフィグボタン
[button name="role_button" role="sleepgame" graphic="../others/plugin/theme_kopanda_bth_02/image/button/sleep.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/sleep2.png" storage="../others/plugin/theme_kopanda_bth_02/config.ks" x="1000" y="686"]

; メッセージウィンドウ非表示ボタン
[button name="role_button" role="window" graphic="../others/plugin/theme_kopanda_bth_02/image/button/close.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/close2.png" x="1090" y="686"]

; タイトルに戻るボタン
[button name="role_button" role="title" graphic="../others/plugin/theme_kopanda_bth_02/image/button/title.png" enterimg="../others/plugin/theme_kopanda_bth_02/image/button/title2.png" x="1170" y="686"]

[endmacro]

;=================================================================================

;　システムで利用するHTML,CSSの設定

;=================================================================================

; セーブ画面
[sysview type="save" storage="./data/others/plugin/theme_kopanda_bth_02/html/save.html"]

; ロード画面
[sysview type="load" storage="./data/others/plugin/theme_kopanda_bth_02/html/load.html"]

; バックログ画面
[sysview type="backlog" storage="./data/others/plugin/theme_kopanda_bth_02/html/backlog.html"]

; メニュー画面
[sysview type="menu" storage="./data/others/plugin/theme_kopanda_bth_02/html/menu.html"]

; CSS
[loadcss file="./data/others/plugin/theme_kopanda_bth_02/bth02.css"]


;=================================================================================

;　テストメッセージ出力プラグインの読み込み

;=================================================================================
[loadjs storage="plugin/theme_kopanda_bth_02/testMessagePlus/gMessageTester.js"]
[loadcss file="./data/others/plugin/theme_kopanda_bth_02/testMessagePlus/style.css"]

[macro name="test_message_start"]
[eval exp="gMessageTester.create()"]
[endmacro]

[macro name="test_message_end"]
[eval exp="gMessageTester.destroy()"]
[endmacro]

[macro name="test_message_reset"]
[eval exp="gMessageTester.currentTextNumber=0;gMessageTester.next(true)"]
[endmacro]


[return]
