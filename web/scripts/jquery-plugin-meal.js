(function ($) {

    $.fn.LayoutPanel = {

        defaultConfig: {
            /** toolbar所在Layout的ID */
            toolbar_layout_id: '',

            toolbar_operate: [{ title: 'aaa', iconCls: 'icon.left' }],
            option: {
				/**
				 * 查询操作
				 */
                query: {

                },
				/**
				 * 删除操作
				 */
                del: {

                },
				/**
				 * 修改操作
				 */
                update: {

                }
            }
        },
        event: {
            initEvent: function () {
                var operateArray = $.fn.LayoutPanel.defaultConfig.toolbar_operate;

                //添加操作到toolbar
                var toolbarRegionId = $.fn.LayoutPanel.defaultConfig.toolbar_layout_id;

                var operateStr = "";
                for (var i = 0; i < operateArray.length; i++) {
                    operateStr += '<a href="#" class="easyui-linkbutton" data-options="iconCls:\'' + operateArray[i].iconCls + '\',plain:true">' + operateArray[i].title + '</a>';
                }
                $(toolbarRegionId).children().append(operateStr);
                $.parser.parse(toolbarRegionId);

            }
        },
        method: {
            initLayout: function (config) {
                //继承config
                $.extend($.fn.LayoutPanel.defaultConfig, config);

                $.fn.LayoutPanel.event.initEvent();


            }
        }
    }
})($)