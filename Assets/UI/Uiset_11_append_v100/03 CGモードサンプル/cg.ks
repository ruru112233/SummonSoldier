;===========================================================
; CGモード画面作成
;===========================================================

; image フォルダに append_theme フォルダを配置して使用します
; 必要に応じて枚数やページの増減をおこなってください

;-----------------------------------------------------------
*start
;-----------------------------------------------------------
; 初期化
[layopt layer=message0 visible=false]
[layopt layer=0 visible=true]
[layopt layer=1 visible=true]
[hidemenubutton]

[clearfix]
[cm]

; ギャラリーモードの背景読込み
[bg storage=../image/append_theme/bg_gallery.png time=300]

[iscript]

tf.page              = 0   // ページ番号
tf.selected_cg_image = []  // 選択したCGの差分を格納した配列変数
tf.cg_index          = 0   // 上の配列の要素番号

tf.img_thumbnail = '../image/append_theme/lock.png'; // サムネイルのパス
tf.thumbnail_w   = 288; // サムネイルの幅
tf.thumbnail_h   = 162; // サムネイルの高さ

[endscript]

[jump target=*cgpage]

;-----------------------------------------------------------
*cgpage
;-----------------------------------------------------------
[cm]

; ギャラリーモード終了
[button graphic=append_theme/gallery_close.png enterimg=append_theme/gallery_close2.png target=*backtitle x="-10" y="630"]

; tf.page変数を利用して個別閲覧ボタン作成ラベルにジャンプします
[jump target="& 'page_' + tf.page "]

;-------------------------------------------------------
*page_0
;-------------------------------------------------------

; CG閲覧モード画面1ページ目

; graphic には表示する画像のファイル名
; thumb にはサムネイルとして表示する画像のファイル名（記述がなければgraphicのファイルを指定）

; 一段目
; ▼実際に組み込むときはこのように記述します
;[cg_image_button graphic="room.jpg,rouka.jpg" thumb="room.jpg"  no_graphic="../image/append_theme/lock.png" x=260 y=195 width=240 height=135]
;[cg_image_button graphic="rouka.jpg" thumb="rouka.jpg" no_graphic="../image/append_theme/lock.png" x=520 y=195 width=240 height=135]

[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="184" y="222" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="496" y="222" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="808" y="222" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]

; 二段目
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="184" y="410" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="496" y="410" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="808" y="410" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]

; 次ページへ
[button graphic=append_theme/gallery_next.png enterimg=append_theme/gallery_next2.png target=*nextpage x="1146" y="360"]

; 共通処理にジャンプ
[jump target=*common]

;-------------------------------------------------------
*page_1
;-------------------------------------------------------
; CG閲覧モード画面2ページ目

; 一段目
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="184" y="222" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="496" y="222" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="808" y="222" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]

; 二段目
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="184" y="410" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="496" y="410" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="808" y="410" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]

; 前のページへ
[button graphic=append_theme/gallery_prev.png enterimg=append_theme/gallery_prev2.png target=*backpage x="62" y="360"]

; 次のページへ
[button graphic=append_theme/gallery_next.png enterimg=append_theme/gallery_next2.png target=*nextpage x="1146" y="360"]

; [jump]
; 共通処理にジャンプ
[jump target=*common]

;-------------------------------------------------------
*page_2
;-------------------------------------------------------
; CG閲覧モード画面3ページ目

; 一段目
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="184" y="222" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="496" y="222" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="808" y="222" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]

; 二段目
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="184" y="410" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="496" y="410" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]
[cg_image_button graphic="" thumb="" no_graphic="&tf.img_thumbnail" x="808" y="410" width="&tf.thumbnail_w" height="&tf.thumbnail_h"]

; 前ページへ
[button graphic=append_theme/gallery_prev.png enterimg=append_theme/gallery_prev2.png target=*backpage x="62" y="360"]

; 共通処理にジャンプ
[jump target=*common]

;-------------------------------------------------------
*common
;-------------------------------------------------------

; 停止

[s]


;-----------------------------------------------------------
*backtitle
;-----------------------------------------------------------
; タイトルに戻る処理

; 使用したレイヤーをすべて消去
[cm]
[freeimage layer=0]
[freeimage layer=1]

; 別のシナリオにジャンプする場合はここを変更
[jump storage=title.ks]


;-----------------------------------------------------------
*nextpage
;-----------------------------------------------------------
; 次のページに移る処理

; 一時変数 tf.page を増加させたうえで *cgpage へ
[eval exp=tf.page++]
[jump target=*cgpage]

;-----------------------------------------------------------
*backpage
;-----------------------------------------------------------
; 前のページに移る処理

; 一時変数 tf.page を減少させたうえで *cgpage へ
[eval exp=tf.page--]
[jump target=*cgpage]

;-----------------------------------------------------------
*no_image
;-----------------------------------------------------------
; 未解放のCGをクリックしたときの処理
[jump target=*cgpage]

;-----------------------------------------------------------
*clickcg
;-----------------------------------------------------------
; 解放済みのCGをクリックしたときの処理

; フリーレイヤーとレイヤー１(back)を解放します
[cm]
[freeimage layer=1 page=back]

; 一時変数 tf.cg_index に 0 をぶち込みます
[eval exp="tf.cg_index = 0"]

;-------------------------------------------------------
*cg_next_image
;-------------------------------------------------------
; CGを見ていきます。
; 見るべきCGがひとつしかない場合は、それだけ見たあと *cgpage に戻ります。
; 見るべきCGが複数ある場合(差分がある場合)は、
; 再帰的にこのラベルに飛び直して次のCGを見ていきます。

; 一時変数 tf.storage に表示すべきCGのstorageを代入します
[iscript]
tf.storage = tf.selected_cg_image[tf.cg_index];
[endscript]

; CGを表示してクリックを待ちます。
[freeimage layer=1 page=back]
[image     layer=1 page=back storage=&tf.storage folder=bgimage width=1280 height=720]
[trans     layer=1 time=700]
[wt]
[l]

; クリックされたら
; 一時変数 tf.cg_index (差分画像がある場合の画像番号)を1増加させます。
[eval exp=tf.cg_index++]

; まだ表示すべき差分画像が残っているなら、このラベルに飛びなおします。
[if exp=" tf.selected_cg_image.length > tf.cg_index "]
  [jump target=cg_next_image]

[else]
  [freeimage layer=1 page=back]
  [freeimage layer=1 page=fore time=700]
  [jump target=*cgpage]

[endif]
