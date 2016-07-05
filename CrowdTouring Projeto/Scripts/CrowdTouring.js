var caracteres = 50;
$(document).ready(function () {
    $("#checky").click(function () {
        $("#RegisterBut").attr("disabled", !this.checked);
    });


    $("#checky2").click(function () {
        $("#RegisterBut2").attr("disabled", !this.checked);
    });

    $("#TipoUtilizador").change(function () {
                   console.log("a");
        if ($(this).val() == "Cliente") {
            $("#TextoTipoUtilizador").text('Crie desafios de inovação turística e obtenha melhores soluções a um preço mais acessível e de melhor qualidade');
        }
        if ($(this).val() == "Resolvedor") {
            $("#TextoTipoUtilizador").text('Crie Soluções para os desafios e obtenha uma grande variadade de prémios');
        }
        if ($(this).val() == "Avaliador") {
            $("#TextoTipoUtilizador").text('Avalie as soluções e ganhe notoriedade na plataforma');
        }
    });

    $("div.myclass").hover(function () {
        $(this).css("background-color", "red")
    });

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#fotografia').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#ImagePath").change(function () {
        console.log("x");
        readURL(this);
    });

    $(".caixaUser").mouseover(function () {
        $(this).css({"background-color":"#cce6ff"});
    }).mouseout(function () {
        $(this).css("background-color", "transparent");
    });

    $("#submeterUtilizador").submit(function (event) {
        console.log("a");
        var utilizadorNome1 = $("#srch-term").val();
        var utilizadorNome2 = $("#Utilizador").val();
        if(utilizadorNome1 == utilizadorNome2)
            $.post("/Account/EditarUtilizador" + utilizadorNome1);
        else 
            $.post("/Account/VisualizarPerfilUtilizador" + utilizadorNome1);
    });
});