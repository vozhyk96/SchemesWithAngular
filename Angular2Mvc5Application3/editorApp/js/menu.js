/*
 * wybieranie narzędzia z menu
 */

$( document ).ready(function() {
	$(".navbar-nav li").click(function() {
		$(".navbar-nav li").removeClass("active");
		$(".navbar-nav li").removeClass("activePen");
	    $(this).addClass('active');
	    $(this).addClass('activePen');
	});
});


