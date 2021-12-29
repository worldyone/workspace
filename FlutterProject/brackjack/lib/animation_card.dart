import 'package:flutter/material.dart';

/*
 * アニメーショントランプ
 */
class AnimationCard extends StatefulWidget {
  final Image frontImage;

  AnimationCard(this.frontImage) : super();

  @override
  _AnimationCardState createState() => _AnimationCardState();
}

/*
 * アニメーショントランプ ステート
 */
class _AnimationCardState extends State<AnimationCard>
    with TickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _frontAnination;
  late Animation<double> _backAnination;

  final Image _backImage = Image.asset("assets/cards/card_back.png");

  @override
  void initState() {
    super.initState();

    /*
     * アニメーションコントローラ
     */
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(
        milliseconds: 200,
      ),
    );

    /*
     * トランプの表アニメーション
     */
    _frontAnination = Tween(
      begin: 1.0,
      end: 0.0,
    ).animate(
      CurvedAnimation(
        parent: _controller,
        curve: const Interval(
          0.0,
          0.5,
          curve: Curves.easeIn,
        ),
      ),
    );

    /*
     * トランプの裏アニメーション
     */
    _backAnination = CurvedAnimation(
      parent: _controller,
      curve: const Interval(
        0.5,
        1.0,
        curve: Curves.easeOut,
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 100.0,
      child: GestureDetector(
        /*
         * トランプタッチジェスチャー
         */
        onTap: () {
          setState(() {
            if (_controller.isCompleted || _controller.velocity > 0) {
              _controller.reverse();
            } else {
              _controller.forward();
            }
          });
        },
        child: Stack(
          children: <Widget>[
            /*
             * トランプ表
             */
            AnimatedBuilder(
              child: widget.frontImage,
              animation: _backAnination,
              builder: (BuildContext context, Widget? child) {
                return _getCardTransform(
                  child!,
                  _backAnination.value,
                );
              },
            ),

            /*
             * トランプ裏
             */
            AnimatedBuilder(
              child: _backImage,
              animation: _frontAnination,
              builder: (BuildContext context, Widget? child) {
                return _getCardTransform(
                  child!,
                  _frontAnination.value,
                );
              },
            ),
          ],
        ),
      ),
    );
  }

  Transform _getCardTransform(Widget child, dynamic value) {
    final Matrix4 transform = Matrix4.identity()..scale(value, 1.0, 1.0);

    return Transform(
      transform: transform,
      alignment: FractionalOffset.center,
      child: child,
    );
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }
}
