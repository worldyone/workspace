const ModName = "自動クリックMOD";

Game.registerMod("auto click mod",{
	init:function(){
		Game.Notify(ModName, "Developed by kanikun", [33, 4]);
		
		setInterval(function(){
			Game.ClickCookie(0);
		}, 3);
		
		setInterval(function(){
			// ゴールデンが出ていたらクリック
			if (Game.shimmers.length > 0) {
				while(Game.shimmers.length>0){
					Game.shimmers[0].l.click();
				}
			}
		},1000);
	},
	save:function(){
	},
	load:function(str){
	},
});
