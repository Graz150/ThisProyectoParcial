$('.menu-bar').on('click', function () {
    $('.sidebar').toggleClass('abrir');
});
/*
$('.menu-bar').on('click', function () {
    $('.contenido').toggleClass('abrircontenido');
});
*/

    $('.contenidosd').on('click', function(e){
        e.preventDefault();
        var SubMenu=$(this).next('ul');
        var iconBtn=$(this).children('.zmdi-caret-down');
        if(SubMenu.hasClass('show-sideBar-SubMenu')){
            iconBtn.removeClass('zmdi-hc-rotate-180');
            SubMenu.removeClass('show-sideBar-SubMenu');
        }
    });
