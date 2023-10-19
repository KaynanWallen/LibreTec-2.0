function OcultarSettings() {
    var Settings = document.getElementById("Settings");
    Settings.style.display = "none"
    var MenuSettings = document.getElementById("Menu-Settings");
    MenuSettings.style.display = "flex"
}

function OcultarMenuSettings() {
    var Settings = document.getElementById("Settings");
    Settings.style.display = "flex"
    var MenuSettings = document.getElementById("Menu-Settings");
    MenuSettings.style.display = "none"
}


var livros = [];
var UltimaPesquisa = '';


function MostrarLivrosDevolver() {
    console.log(livros)
    DevolverLivro()
}

function MostrarLivrosRemover() {
    console.log(livros)
    RemoverLivro()
}

$(document).ready(function () {

    getDatatable('#table-biblioteca')
});


function enviarParametrosParaServidor() {
    
    var pesquisa = document.getElementById("CampoPesquisa").value;
    if (pesquisa == "") {
        alert("Digite algo no campo de pesquisa")
        return
    }
    var campo = document.getElementById("Campo").value;
    if (campo == "Opções") {
        alert("Selecione algum campo")
        return
    }
    livros = [];
    var conteinerHtml = document.getElementById("LocalCardsResultados");
    conteinerHtml.innerHTML = "";

    var AvisoNadaEncontrado = document.getElementById("NadaEncontrado");
    AvisoNadaEncontrado.style.display = 'none';

    var IconePesquisando = document.getElementById("LoadingPesquisa");
    IconePesquisando.style.display = '';

    var LocalParaAvisos = document.getElementById("LocalParaAvisos");
    LocalParaAvisos.style.display = 'flex';
    
    console.log("Pesquisando")

    $.ajax({
        type: 'POST',
        url: '/Livro/PesquisarLivros', // Substitua pelo URL do seu controlador
        data: { Pesquisa: pesquisa, Campo: campo },
        success: function (data) {
            // Lide com a resposta do servidor (por exemplo, atualize a página)
            IconePesquisando.style.display = 'none';
            if (data.livros.result.lenght == null) {
                var AvisoNadaEncontrado = document.getElementById("NadaEncontrado");
                AvisoNadaEncontrado.style.display = '';
            }
            console.log(data.livros.result)
            for (const objeto of data.livros.result) {
                //console.log(objeto)
                LocalParaAvisos.style.display = 'none';
                livros.push(objeto)
                InserirHtml(objeto.titulo, objeto.tombo_Atual, objeto.emprestado.nome, objeto.emprestado.ra, objeto.id)

            };
        },
        error: function (error) {
            // Lide com erros, se houver
            console.error(error);
        }
    });
}

function AdicionarMuitosLivros() {
    $.ajax({
        type: 'GET',
        url: '/Livro/AdicionarTodosLivrosExcel', // Substitua pelo URL do seu controlador
        success: function (data) {
            console.log(data)
        },
        error: function (error) {
            // Lide com erros, se houver
            console.error(error);
        }
    });
}

function DevolverLivro() {
    var TextoDoAlert = document.getElementById("TextoDoAlert");
    var DivAlertDanger = document.getElementById("DivAlertDanger");
    var DivAlert = document.getElementById("DivAlert");
    var CarregandoDevolverLivros = document.getElementById("CarregandoDevolverLivros");

    DivAlert.style.display = 'none';
    DivAlertDanger.style.display = 'none';
    CarregandoDevolverLivros.style.display = '';



    for (const livro of livros) {
        $.ajax({
            type: 'POST',
            url: '/Livro/DevolverLivro', // Substitua pelo URL do seu controlador
            data: { id: livro.id },
            success: function (data) {
                enviarParametrosParaServidor();
                TextoDoAlert.textContent = "Devolvido com sucesso"
                DivAlert.style.display = 'flex'
                CarregandoDevolverLivros.style.display = 'none';
            },
            error: function (error) {
                // Lide com erros, se houver
                console.error(error);
                TextoDoAlert.textContent = "Ops, Algo deu errado, tente novamente"
                DivAlertDanger.style.display = 'flex';
                CarregandoDevolverLivros.style.display = 'none';
            }
        });
    }
}


function RemoverLivro() {
    var TextoDoAlert = document.getElementById("TextoDoAlert");
    var DivAlertDanger = document.getElementById("DivAlertDanger");
    var DivAlert = document.getElementById("DivAlert");
    var CarregandoDevolverLivros = document.getElementById("CarregandoDevolverLivros");

    DivAlert.style.display = 'none';
    DivAlertDanger.style.display = 'none';
    CarregandoDevolverLivros.style.display = '';



    for (const livro of livros) {
        $.ajax({
            type: 'POST',
            url: '/Livro/DeleteLivro', // Substitua pelo URL do seu controlador
            data: { id: livro.id },
            success: function (data) {
                console.log(data);
                enviarParametrosParaServidor();
                TextoDoAlert.textContent = "Removido com sucesso"
                DivAlert.style.display = 'flex'
                CarregandoDevolverLivros.style.display = 'none';
            },
            error: function (error) {
                // Lide com erros, se houver
                console.error(error);
                TextoDoAlert.textContent = "Ops, Algo deu errado, tente novamente"
                DivAlertDanger.style.display = 'flex';
                CarregandoDevolverLivros.style.display = 'none';
            }
        });
    }
}


function InserirHtml(titulo, tombo, aluno, ra, id) {
    var novoHtml = "";

    
    novoHtml += '<div style="cursor:pointer;background-color:transparent;width:550px;min-height:150px;padding:10px;display:flex;flex-direction:column;justify-content:space-between">';
    novoHtml += '<div>';
    novoHtml += '<section style="display:flex;flex-direction:row;height:auto">';
    novoHtml += '<p style="font-weight:bold">Livro:</p>';
    novoHtml += '<span>' + titulo + '</span>';
    novoHtml += '</section>';

    novoHtml += '<section style="display:flex;flex-direction:row;height:20px">';
    novoHtml += '<p style="font-weight:bold">Tombo:</p>';
    novoHtml += '<span>' + tombo + '</span>';
    novoHtml += '</section>';

    novoHtml += '<section style="display:flex;flex-direction:row;height:20px">';
    novoHtml += '<p style="font-weight:bold">Aluno:</p>';
    novoHtml += '<span>' + aluno + '</span>';
    novoHtml += '</section>';

    novoHtml += '<section style="display:flex;flex-direction:row;height:20px">';
    novoHtml += '<p style="font-weight:bold">RA:</p>';
    novoHtml += '<span>' + ra + '</span>';
    novoHtml += '</section>';

    novoHtml += '</div>';
    novoHtml += ' <div style="display:flex;flex-direction:row;justify-content:flex-end;align-items:flex-end;gap: 20px;padding-right:20px">';
    novoHtml += '<section style="background-color: #3ACF1F; width:100px; height:30px;border-radius:10px;display:flex;justify-content:center;align-items:center">';
    novoHtml += '<span style="font-weight:700">20/12/2023</span>';
    novoHtml += '</section>';
    novoHtml += '<section style="background-color: #B78718; width:100px; height:30px;border-radius:10px;display:flex;justify-content:center;align-items:center">';
    novoHtml += ' <span style="font-weight:700">20/12/2023</span>';
    novoHtml += '</section>';
    novoHtml += '</div>';
    novoHtml += '</div>'


    var Card = document.createElement("section");
    Card.className = "CardDevolver";
    Card.id = id;
    Card.style.backgroundColor = '#fff';
    Card.onclick = function () {
        // Chame a função desejada quando o elemento for clicado
        SelecionarCard(id)
    };
    Card.innerHTML = novoHtml;
    var conteinerHtml = document.getElementById("LocalCardsResultados");
    
    conteinerHtml.appendChild(Card);
}

function SelecionarCard(id) {
    var card = document.getElementById(id)
    var corAtual = card.style.backgroundColor;

    if (corAtual == "rgb(0, 92, 109)") {
        card.style.backgroundColor = '#ffffff';
    }else if (corAtual == "rgb(255, 255, 255)") {
        card.style.backgroundColor = '#005C6D';
    }
}

$('.close-alert').click(function () {
    $('.alert').hide('hide');
});



function getDatatable(id) {
    $(id).DataTable({
        "lengthMenu": [[9, 25, 50, -1], [9, 25,50,"All"]],
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}