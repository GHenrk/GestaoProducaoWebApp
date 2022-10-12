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
        detalhesCard.classList.add('cardText');

        if (data[i].maquinaAtiva == true) {
            const tempoTotal = [data[i].totalHoras, data[i].totalMinutos, data[i].totalSegundos]
            console.log(tempoTotal)
            
            var codigoProcesso = document.createElement('p');
            codigoProcesso.classList.add('codigoProcesso--card');
            codigoProcesso.append(`Apontamento: ${data[i].aptMaquina}`)

            var codigoOp = document.createElement('p');
            codigoOp.classList.add('codigoOp--card')
            codigoOp.append(`OP: ${data[i].op}`)

            var tempoDecorrido = document.createElement('p');
            tempoDecorrido.classList.add('tempoTotal--card');
            tempoDecorrido.id = i;
            tempoDecorrido.append(`${tempoTotal[0]}:${tempoTotal[1]}:${tempoTotal[2]}`);

            var quantidadeParadas = document.createElement('p');
            quantidadeParadas.classList.add('qntdParadas--card');
            quantidadeParadas.append(`Quantidade Paradas: ${data[i].qntdParadas}`);


            detalhesCard.appendChild(codigoProcesso);
            detalhesCard.appendChild(codigoOp);
            detalhesCard.appendChild(quantidadeParadas);
            detalhesCard.appendChild(tempoDecorrido);

            mostraTempo(tempoTotal, i);
        }


        var status = document.createElement('p');
        status.classList.add('status--card');
        status.append(`Status: ${data[i].status}`);

        if (data[i].status == "Ativo") {
            card.classList.toggle('bgCardAtivo');
        }

        if (data[i].status == "Parado") {
            card.classList.add('bgCardDanger');
        }


        detalhesCard.appendChild(status);

        card.appendChild(detalhesCard);

        divLista.appendChild(card);


    }


};



function mostraTempo(tempoTotal, idMaquina) {

    var horas = tempoTotal[0];
    var minutos = tempoTotal[1]
    var segundos = tempoTotal[2]

    var meuInterval = setInterval(() => {
        var tempoTotalDisplay = document.getElementById(`${idMaquina}`);
        tempoTotalDisplay.innerHTML = " ";

        segundos += 1;
        if (segundos > 59) {
            minutos += 1;
            segundos = 0;
        }
        if (minutos > 59) {
            horas += 1;
            minutos = 0;
        }
        
        horasFormat = horas < 10 ? "0" + horas : horas;
        minutosFormat = minutos < 10 ? "0" + minutos : minutos;
        segundosFormat = segundos < 10 ? "0" + segundos : segundos;
        var tempoFormatado = `${horasFormat}:${minutosFormat}:${segundosFormat}`;
        tempoTotalDisplay.append(tempoFormatado);
    }, 1000);
    

    

}

