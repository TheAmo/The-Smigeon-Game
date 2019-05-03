/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

$(document).ready(function () { 
    var style = { 
        "font-family":"serif",
        "font-size": "20px",                  
    }; 
    $(".slider").addClass("w3-sidebar w3-bar-block");

    $(".sousClique").css(style); 
    $(".sousClique").addClass("w3-bar-item w3-button");      
    $('.sousClique').bind('click', function(){
        //alert($(this).attr('id')); // gets the id of a clicked link that has a class of menu
        $(".sousClique").attr("href", $(this).attr('id')); 
    });                
});
