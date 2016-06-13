$(document).ready(function () {
    $("#CriarDesafio").click(function () {
        $("#modal").load("Create", function () {
            $("#modal").modal();
        })
    });

    $(".VisualizarSolucao").click(function (event) {
        var url = $(this).data("url");
        console.log(url);
        $("#modal").load(url,  function () {
            $("#modal").modal();
        })
        event.preventDefault();
    });
});


