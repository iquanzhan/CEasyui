$().ready(function () {

    $.nameplace("com.zsat.meal");

    $.extend(com.zsat.meal, $.fn.LayoutPanel);

    com.zsat.meal.method.initLayout({
        toolbar_layout_id: '#toolbarRegion',
        toolbar_operate: [{ title: '新增', iconCls: 'icon-add' }, { title: '删除', iconCls: 'icon-add' }],
    });


});