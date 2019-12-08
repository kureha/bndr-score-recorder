# BNDR Score Recorder

リズムゲーム等のスクリーンショット画像をスキャンし、曲名・結果ノーツ数・最大コンボ等の情報を文字情報として抽出し、データベース化して閲覧可能にするソフトウェアです。
詳細結果の記録機能が無いリズムゲーム等で、結果入力の手間を大幅に簡略化することを目的としています。

以下では一部、[BanG Dream！ガールズバンドパーティー！](https://bang-dream.bushimo.jp/)のゲーム内スクリーンショットが使用されています。

## Demo

![Demo01](https://user-images.githubusercontent.com/175231/70383546-0a3f9180-19b3-11ea-9a4d-ee34a905c955.jpg)

## Requirements

* OS: Windows10
* Resolution: Minimum 1280x720
* External Softwae
	* [ImageMagick 7.0.0+](https://imagemagick.org/script/download.php)
	* [Tesseract at UB Mannheim 5.0.0+](https://github.com/UB-Mannheim/tesseract/wiki)

## Install

* BNDR Score Recorder
配布されたZipフォルダ解凍してください。

* External Software
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

4. 「3.」のファイルを使用し、画像切り取り画面が表示される。5つの情報の画像読み込み位置を画像上でクリック＆ドラッグで指定し、Position x Sizeで調整する。

![Usage04-01](https://user-images.githubusercontent.com/175231/70383547-0a3f9180-19b3-11ea-8049-23bc189b0316.jpg)

5. 「4.」で調整したら「Try to Analyze」で試験切り出しをし、読み取り結果が期待される出力内容の読み取り結果のようになっているか？を確認する。問題なければ「↑ SAVE」ボタンで設定をメモリ上に仮保存する。

6. 5つの情報を設定し終えたら「Save Setting」で設定ファイルに保存する。

7. メニューから「スコア解析」→「解析実行」をクリックし、スクリーンショット画像の入ったディレクトリを指定する。

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

君は[Issues](https://github.com/kureha/bndr-score-recorder/issues)に登録してもよいし、作者の[Twitter](https://twitter.com/atodelie)に言ってもいいし、このソフトウェアはOSSなので自分で修正してPull Requestを出してもよいのです。

## Author

[Kureha Hisame](https://twitter.com/atodelie)
