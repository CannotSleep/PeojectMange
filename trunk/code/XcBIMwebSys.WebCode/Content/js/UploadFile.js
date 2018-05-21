//文件上传js
var type = "";
var introduce = "";


layui.use(['jquery', 'form', 'upload'], function () {
    var $ = layui.$,
        form = layui.form,
        upload = layui.upload;

    upload.render({
        elem: '#upbtn', //绑定元素
        url: '/Material/UploadFile',
        accept: 'file',
        // method: 'post',
        auto: false,
        bindAction: '#upsub',
        data: {
            "type": type + 123456,
            "introduce": introduce + 79789
        },
        choose: function (obj) {
            obj.preview(function (index, file, result) {
                console.log(index);
                console.log(file);
                console.log(result);
                $('#FileName').html(file.name);
            });
        },
        before: function (obj) {
            obj.preview(function (index, file, result) {
                console.log(index);
                console.log(file);
                console.log(result);

                $('#FileName').html(file.name);

            });
            type = $("#fileType option:selected").val();
            introduce = $("#fileIntroduce").val();

            this.data = { "type": type, "introduce": introduce };

        },
        done: function (res, index) {

            //上传完毕回调
            if (JSON.stringify(res) == 1||res==1) {
                alert("上传成功");
            } else {
                alert("上传失败")
            }
        }
        ,
        error: function (res) {
            //请求异常回调
            alert("请求异常");
        }

    });
});

