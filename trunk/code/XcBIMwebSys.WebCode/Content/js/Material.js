//文件管理js
var tableIns;
layui.use(['jquery', 'table', 'form', 'laypage'], function () {
    var $ = layui.$,
        table = layui.table,
        form = layui.form;
    laypage = layui.laypage;


    tableIns = table.render({
        elem: '#FileTable',
        cols: [
            [{
                checkbox: true
            }, {
                field: 'tbid',
                title: 'ID',
                align: 'center',
                width: 300
            }, {
                field: 'tbfileType',
                title: '分类',
                align: 'center',
                width: 100
            }, {
                field: 'tbnamebefore',
                title: '文件名',
                align: 'center',
                width: 100
                //templet: function (d) {
                //    return '<i class="layui-icon">&#xe621;</i><a download="' + d.FileName + '" style="color:blue" href="' + d.DownloadUrl + '">' + d.FileName + '</a>';
                //}
            }, {
                field: 'tbfileExplain',
                title: '说明',
                align: 'center'
            }, {
                field: 'tbusername',
                title: '上传人',
                align: 'center',
                width: 100
            }, {
                field: 'tbdate',
                title: '上传时间',
                align: 'center',
                width: 180
            }, {
                fixed: 'right',
                align: 'center',
                toolbar: '#barDemo',
                width: 150
            }]
        ],
        initSort: {
            field: 'tbdate',
            type: 'desc'
        },
        where: {},
        headers: {
            page: 1,
            limit: 15
        },
        limit: 15,
        limits: [15, 30, 45, 60, 75, 90],
        page: laypage, //开启分页

        loading: true,
        url: '/Material/getfileList', //数据接口
        method: 'get',
        done: function (res, curr, count) {
            //得到当前页码
            console.log(curr);

            //得到数据总量
            console.log(count);

        }
    })

    //监听表格复选框选择
    table.on('checkbox(FileTable)', function (obj) {
        console.log(obj)
    });
    //监听工具条
    table.on('tool(FileTable)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') {
            fileShow(data.tbnamebefore, data.tbaddress, data.tbtype);
            // layer.msg('ID：' + data.tbnamebefore + ' 的查看操作');

        } else if (obj.event === 'del') {
            layer.confirm('真的删除行么', function (index) {
                obj.del();
                fileDelete(data.tbnamebefore);
                layer.close(index);
            });
        } else if (obj.event === 'down') {
            fileDown(data.tbnamebefore)
            //layer.alert('编辑行：<br>' + JSON.stringify(data))

        }
    });

    var $ = layui.$, active = {
        getCheckData: function () { //获取选中数据
            var checkStatus = table.checkStatus('idTest')
                , data = checkStatus.data;
            layer.alert(JSON.stringify(data));
        }
        , getCheckLength: function () { //获取选中数目
            var checkStatus = table.checkStatus('idTest')
                , data = checkStatus.data;
            layer.msg('选中了：' + data.length + ' 个');
        }
        , isAll: function () { //验证是否全选
            var checkStatus = table.checkStatus('idTest');
            layer.msg(checkStatus.isAll ? '全选' : '未全选')
        }
    };

    $('.demoTable .layui-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
})


///文件删除方法
function fileDelete(param2) {

    $.ajax({
        url: "/Material/deleteFileByFileName", //CommonController下的CancelTaskDeal方法
        type: "get",
        async: false,
        cache: false,
        dataType: "json",
        data: {
            name: param2
        },
        success: function (r) { //没有异常，获取返回值  r 为FeedbackModel 对象
            //上传完毕回调
            if (JSON.stringify(r) == 1) {
                alert("删除成功");
            } else {
                alert("删除失败")
            }

        },
        error: function (err) {    //url无效，请求失败；有Exception异常，没有捕获时。
            //showPromptModel("处理失败");
            alert(err);
        }
    });

}

//文件下载方法
function fileDown(param2) {
    //alert(param1)

    // alert(myArray[param1].tb_name_now);
    //var name1= myArray[param1].tb_name_now


    var url = '@Url.Content("~/Material/downloadFile")';//下载文件你的action

    var form = $("<form>");

    form.attr('style', 'display:none');

    form.attr('target', '');

    form.attr('method', 'post');

    form.attr('action', '/Material/downloadFile');

    var input1 = $('<input>');

    input1.attr('type', 'hidden');

    input1.attr('name', 'fileName');

    input1.attr('value', param2);

    form.append(input1);

    $('body').append(form);

    form.submit();

    //form.remove();
}


function fileShow(param1, param2, param3) {

    switch (param3) {
        case 0:
            //打开图片

            var str = param2 + "\\" + param1;

            var strName = param1;

            document.cookie = 'picname=' + param1;

            window.open("ViewPic");

            break;

        case 1:
            //打开word、excel文档
            var strName = param1;

            var str = param2 + "\\" + param1;

            document.cookie = 'docname=' + param1;

            window.open("DocView");

            break;

        default:
            break;
    }

}


function btnSearch() {

    var userName = $("#upUser").val();

    var fileName = $("#fileName").val();

    var projectType = $("#projectType option:selected").val();



    if (userName == "") {
        alert("请填写上传人");
    }

    if (fileName == "") {
        alert("请填写文件名");
    }

    if (projectType == "") {
        alert("请选择文件分类");
    }

    //if (userName != "" && fileName != "" && projectType!="") {
    tableIns.reload({
        where: { //设定异步数据接口的额外参数，任意设
            userName: userName,
            fileName: fileName,
            projectType: projectType
        }
        , page: {
            curr: 1 //重新从第 1 页开始
        }
    });
    //}

}
