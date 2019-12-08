# BNDR Score Recorder

リズムゲーム等のスクリーンショット画像をスキャンし、曲名・結果ノーツ数・最大コンボ等の情報を文字情報として抽出し、データベース化して閲覧可能にするソフトウェアです。  
詳細結果の記録・閲覧機能が無いリズムゲーム等で、結果入力の手間を大幅に簡略化しながら、詳細結果を記録・閲覧する機能を実現します。  
  
文字情報抽出にはTesseract-OCR（画像認識）を使用しますが、不完全な読み取りと認識した場合には、必要に応じて操作者に警告する機能が実装されています。  
また一部の情報はユーザの補正入力に対し、既存データとのマッチングを行い、自動的に画像認識データの最適化を実施します。

以下では一部、[BanG Dream！ガールズバンドパーティー！](https://bang-dream.bushimo.jp/)のゲーム内スクリーンショットが使用されています。

## Demo

![Demo01](https://user-images.githubusercontent.com/175231/70384347-a4f29d00-19c0-11ea-9ff8-c88750cafc01.jpg)  
![Demo02](https://user-images.githubusercontent.com/175231/70384378-16cae680-19c1-11ea-8d03-58416f0a4f2c.jpg)

## Requirements

* OS: Windows10
* Resolution: Minimum 1280x720
* External Softwae
	* [Microsoft .NET Framework 4.7.2+](https://dotnet.microsoft.com/download)
	* [ImageMagick 7.0.0+](https://imagemagick.org/script/download.php)
	* [Tesseract at UB Mannheim 5.0.0+](https://github.com/UB-Mannheim/tesseract/wiki)

## Install

* BNDR Score Recorder  
[Releaseページ](https://github.com/kureha/bndr-score-recorder/releases)からダウンロードしたバイナリファイルを解凍するだけです。

* External Software
	* Microsoft .NET Framework 4.7.2+  
	<https://docs.microsoft.com/ja-jp/dotnet/framework/install/on-windows-10> の通りにインストールすれば問題ありません。  
	* ImageMagick 7.0.0+  
	<https://imagemagick.org/script/download.php> の下にある「ImageMagick-x.x.x.x-portable-Q16-x64.zip」からダウンロードして適当なフォルダに解凍。  
	プロジェクトでは過去Verの「<https://imagemagick.org/download/binaries/ImageMagick-7.0.9-8-portable-Q16-x64.zip>」を推奨。  
	* Tesseract at UB Mannheim 5.0.0+  
	<https://github.com/UB-Mannheim/tesseract/wiki> からダウンロードしてインストール。  
	プロジェクトでは過去Verの「<https://digi.bib.uni-mannheim.de/tesseract/tesseract-ocr-w64-setup-v5.0.0-alpha.20191030.exe>」を推奨。  

	
## Usage

1. BndrScoreRecorder.exeを起動する。
2 .外部EXEが要求されるので、事前に配置＆インストールしたImageMagickのconvert.exeと、Tesseract-OCRのtesseract.exeを指定する。
![Usage02-01](https://user-images.githubusercontent.com/175231/70383549-0a3f9180-19b3-11ea-9a75-25b4a4a825d3.jpg)
3. メニューから「環境設定」→「画像切り抜き座標設定」をクリックし、解析したいリズムゲーム等のスクリーンショット画像ファイルを指定する。
![Usage03-01](https://user-images.githubusercontent.com/175231/70383550-0a3f9180-19b3-11ea-8334-460f7a4c5981.jpg)
4. 「3.」のファイルを使用し、画像切り取り画面が表示される。6つの情報の画像読み込み位置を画像上でクリック＆ドラッグで指定し、Position x Sizeで調整する。
![Usage04-01](https://user-images.githubusercontent.com/175231/70384348-a4f29d00-19c0-11ea-94b1-26f3e1d57ea7.jpg)
5. 「4.」で調整したら「Try to Analyze」で試験切り出しをし、読み取り結果が期待される出力内容の読み取り結果のようになっているか？を確認する。問題なければ「↑ SAVE」ボタンで設定をメモリ上に仮保存する。
6. 6つの情報を設定し終えたら「Save Setting」で設定ファイルに保存する。
7. メニューから「スコア解析」→「解析実行」をクリックし、スクリーンショット画像の入ったディレクトリを指定する。

## Update plan

* スコアリザルト表にソート機能の実装。
* TreeViewのモード切替。Level順だけではなく、曲名ベース等。
* スコアリザルトのImport/Export。
* 「画像切り抜き座標設定」を複数保持可能化。複数レイアウトの入力に対応（タブレット＆スマホ両持ちプレイヤー向け）。

## License

    The code in this repository is licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.

## Support

もし不具合を見つけたり機能要望があったら、君は[Issues](https://github.com/kureha/bndr-score-recorder/issues)に登録してもよいし、作者の[Twitter](https://twitter.com/atodelie)に言ってもいいし、なんならこのソフトウェアはOSSなので自分で修正してPull Requestを出してもよいのです。

## Author

Kureha Hisame <https://twitter.com/atodelie>
