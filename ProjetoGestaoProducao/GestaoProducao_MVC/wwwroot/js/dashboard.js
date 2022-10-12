//$(document).ready(function () {
//    $.getJSON("dashboard/ListaMaquinas",
//        function (json) {
//            console.log(json)
//        });
//});

async function requisitaDados() {
    //mostraLoading(true);

    await fetch("dashboard/ListaMaquinas")
        .then((response) => response.json())
        .then((data) => {
            console.log("Informações recebidas");
            mostraLista(data);
            console.log(data);
        })
        .catch((error) => {
            console.log("Deu erro" + error);
        })
        .finally(() => {
            //mostraLoading(false);
        });
}


requisitaDados();


function mostraLista(data) {
    var divLista = document.getElementById('divMaquinas');


    //    for (let i = 0; i < data.length; i++) {
    //        divLista.innerHTML += `<div class="card card-escuro mb-3" style="max-width: 20rem;">
    //  <div class="card-header">${data[i].nome}</div>
    //  <div class="card-body">
    //    <h4 class="card-title"></h4>
    //    <p class="card-text">teste.</p>
    //<a href="/Maquinas/Details/${data[i].id}" class="btn btn-sm btn-outline-dark">Máquina Detalhes</a>
    //  </div>`
    //    }

    for (let i = 0; i < data.length; i++) {
        var card = document.createElement('div');
        card.classList.add('cardMaquina');
   

        var tituloCard = document.createElement('h5');
        tituloCard.classList.add('cardTitulo');
        tituloCard.append(`${data[i].nome}`);
        card.appendChild(tituloCard);

        var detalhesCard = document.createElement('div');
        detalhesCard.classList.add('card-text');

        if (data[i].maquinaAtiva == true) {
            var codigoProcesso = document.createElement('p');
            codigoProcesso.classList.add('codigoProcesso--card');
            codigoProcesso.append(`Código Apontamento: ${data[i].aptMaquina}`)

            var codigoOp = document.createElement('p');
            codigoOp.classList.add('codigoOp--card')
            codigoOp.append(`Ordem de Produção: ${data[i].op}`)

            var tempoDecorrido = document.createElement('p');
            tempoDecorrido.classList.add('tempoTotal--card');
            tempoDecorrido.append(`Tempo Decorrido: ${data[i].tempoTotal}`);

            var quantidadeParadas = document.createElement('p');
            quantidadeParadas.classList.add('tempoTotal--card');
            quantidadeParadas.append(`Quantidade Paradas: ${data[i].qntdParadas}`);


            detalhesCard.appendChild(codigoProcesso);
            detalhesCard.appendChild(codigoOp);
            detalhesCard.appendChild(tempoDecorrido);
            detalhesCard.appendChild(quantidadeParadas)

        }
        var status = document.createElement('p');
        status.classList.add('status--card')
        status.append(`Status: ${data[i].status}`)

        detalhesCard.appendChild(status);

        card.appendChild(detalhesCard);

        divLista.appendChild(card);
    }
};