var $, tab, skyconsWeather;
layui.config({
	base: "js/"
}).use(['bodyTab', 'form', 'element', 'layer', 'jquery'], function() {
	var form = layui.form,
		layer = layui.layer,
		element = layui.element;
	$ = layui.jquery;
	tab = layui.bodyTab({
		openTabNum: "50", //最大可打开窗口数量
		url: "json/navs.json" //获取菜单json地址
	});

	//退出
	$(".signOut").click(function() {
		window.sessionStorage.removeItem("menu");
		menu = [];
		window.sessionStorage.removeItem("curmenu");
	})

	//隐藏左侧导航
	$(".hideMenu").click(function() {
		$(".layui-layout-admin").toggleClass("showMenu");
		//渲染顶部窗口
		tab.tabMove();
	})

	//渲染左侧菜单
	tab.render();

	// 添加新窗口
	$("body").on("click", ".layui-nav .layui-nav-item a", function() {
		if($(this).attr("data-url")) {
			//如果不存在子级
			if($(this).siblings().length == 0) {
				addTab($(this));
				$('body').removeClass('site-mobile'); //移动端点击菜单关闭菜单层
			}
		}
		$(this).parent("li").siblings().removeClass("layui-nav-itemed");
	})

	//公告层
	function showNotice() {
		layer.open({
			type: 1,
			title: "系统公告",
			closeBtn: false,
			area: '310px',
			shade: 0.8,
			id: 'LAY_layuipro',
			btn: ['了解'],
			moveType: 1,
			content: '<div style="padding:15px 20px; text-align:left; line-height: 36px; text-indent:0em;border-bottom:1px solid #e2e2e2;"><p>为了方便您的使用,请使用Google Chrome,360极速浏览器等高级浏览器</p></div>',
			success: function(layero) {
				var btn = layero.find('.layui-layer-btn');
				btn.css('text-align', 'center');
				btn.on("click", function() {
					window.sessionStorage.setItem("showNotice", "true");
				})

			}
		});
	}
	//触发系统公告
	$(".showNotice").on("click", function() {
		showNotice();
	})

	//刷新后还原打开的窗口
	if(window.sessionStorage.getItem("menu") != null) {
		menu = JSON.parse(window.sessionStorage.getItem("menu"));
		curmenu = window.sessionStorage.getItem("curmenu");
		var openTitle = '';
		for(var i = 0; i < menu.length; i++) {
			openTitle = '';
			if(menu[i].icon) {
				if(menu[i].icon.split("-")[0] == 'icon') {
					openTitle += '<i class="iconfont ' + menu[i].icon + '"></i>';
				} else {
					openTitle += '<i class="layui-icon">' + menu[i].icon + '</i>';
				}
			}
			openTitle += '<cite>' + menu[i].title + '</cite>';
			openTitle += '<i class="layui-icon layui-unselect layui-tab-close" data-id="' + menu[i].layId + '">&#x1006;</i>';
			element.tabAdd("bodyTab", {
				title: openTitle,
				content: "<iframe src='" + menu[i].href + "' data-id='" + menu[i].layId + "'></frame>",
				id: menu[i].layId
			})
			//定位到刷新前的窗口
			if(curmenu != "undefined") {
				if(curmenu == '' || curmenu == "null") { //定位到后台首页
					element.tabChange("bodyTab", '');
				} else if(JSON.parse(curmenu).title == menu[i].title) { //定位到刷新前的页面
					element.tabChange("bodyTab", menu[i].layId);
				}
			} else {
				element.tabChange("bodyTab", menu[menu.length - 1].layId);
			}
		}
		//渲染顶部窗口
		tab.tabMove();
	}

	//关闭其他
	$(".closePageOther").on("click", function() {
		if($("#top_tabs li").length > 2 && $("#top_tabs li.layui-this cite").text() != "后台首页") {
			var menu = JSON.parse(window.sessionStorage.getItem("menu"));
			$("#top_tabs li").each(function() {
				if($(this).attr("lay-id") != '' && !$(this).hasClass("layui-this")) {
					element.tabDelete("bodyTab", $(this).attr("lay-id")).init();
					//此处将当前窗口重新获取放入session，避免一个个删除来回循环造成的不必要工作量
					for(var i = 0; i < menu.length; i++) {
						if($("#top_tabs li.layui-this cite").text() == menu[i].title) {
							menu.splice(0, menu.length, menu[i]);
							window.sessionStorage.setItem("menu", JSON.stringify(menu));
						}
					}
				}
			})
		} else if($("#top_tabs li.layui-this cite").text() == "后台首页" && $("#top_tabs li").length > 1) {
			$("#top_tabs li").each(function() {
				if($(this).attr("lay-id") != '' && !$(this).hasClass("layui-this")) {
					element.tabDelete("bodyTab", $(this).attr("lay-id")).init();
					window.sessionStorage.removeItem("menu");
					menu = [];
					window.sessionStorage.removeItem("curmenu");
				}
			})
		} else {
			layer.msg("没有可以关闭的窗口了@_@");
		}
		//渲染顶部窗口
		tab.tabMove();
	})
	//关闭全部
	$(".closePageAll").on("click", function() {
		if($("#top_tabs li").length > 1) {
			$("#top_tabs li").each(function() {
				if($(this).attr("lay-id") != '') {
					element.tabDelete("bodyTab", $(this).attr("lay-id")).init();
					window.sessionStorage.removeItem("menu");
					menu = [];
					window.sessionStorage.removeItem("curmenu");
				}
			})
		} else {
			layer.msg("没有可以关闭的窗口了@_@");
		}
		//渲染顶部窗口
		tab.tabMove();
	})
})

//打开新窗口
function addTab(_this) {
	tab.tabAdd(_this);
}