var com = com || {};
com.data = com.data || {};// 用于存放临时的数据或者对象


/**
 * 将form表单元素的值序列化成对象
 * 
 * @example com.serializeObject($('#formId'))
 * 
 * @author lbw
 * 
 * @requires jQuery
 * 
 */
com.serializeObject = function (form, option) {
    var o = {};
    o.length = 0;
    $.each(form.serializeArray(), function (index) {
        if (this['value'] != undefined && this['value'].length > 0) {// 如果表单项的值非空，才进行序列化操作

            if (typeof (o[this['name']]) == "undefined") {
                o[this['name']] = "";
            }

            if (typeof (option) != "undefined" && typeof option.toUpperCase != "undefined" && option.toUpperCase == true) {
                o[this['name']] = o[this['name']] + (o[this['name']] != "" ? "," : "") + $.trim(this['value']).toUpperCase();
            } else if (typeof (option) != "undefined" && typeof option.toUpperCase != "undefined" && option.toLowerCase == true) {
                o[this['name']] = o[this['name']] + (o[this['name']] != "" ? "," : "") + $.trim(this['value']).toLowerCase();
            } else {
                o[this['name']] = o[this['name']] + (o[this['name']] != "" ? "," : "") + $.trim(this['value']).toString();
            }
            o.length = o.length + 1;
        }
    });
    return o;
};