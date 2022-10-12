//$(document).ready(function () {
//    $.getJSON("dashboard/ListaMaquinas",
//        function (json) {
//            console.log(json)
//        });
//});


let listaMaquinas;

async function requisitaDados() {
    //mostraLoading(true);

    await fetch("dashboard/ListaMaquinas")
        .then((response) => response.json())
        .then((data) => {
            console.log("Informações recebidas");
            listaMaquinas = data;
            mostraLista(listaMaquinas);

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
    divLista.innerHTML = " ";

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
            tempoDecorrido.id = data[i].id;
            tempoDecorrido.append(`${tempoTotal[0]}:${tempoTotal[1]}:${tempoTotal[2]}`);

            var quantidadeParadas = document.createElement('p');
            quantidadeParadas.classList.add('qntdParadas--card');
            quantidadeParadas.append(`Quantidade Paradas: ${data[i].qntdParadas}`);


            detalhesCard.appendChild(codigoProcesso);
            detalhesCard.appendChild(codigoOp);
            detalhesCard.appendChild(quantidadeParadas);
            detalhesCard.appendChild(tempoDecorrido);

            mostraTempo(tempoTotal, data[i].id);
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

    criaCarrosel();
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


function resetaIntervals() {
    // Get a reference to the last interval + 1
    const interval_id = window.setInterval(function () { }, Number.MAX_SAFE_INTEGER);

    // Clear any timeout/interval up to that id
    for (let i = 1; i < interval_id; i++) {
        window.clearInterval(i);
    }
}



function criaCarrosel() {

    $('.carousel').slick({
        dots: true,
        infinite: true,
        speed: 500,
        autoplay: true,
        autoplaySpeed: 5000,
        rows: 2,
        slidesPerRow: 3,
        variableWidth: false,
        adaptiveHeight: false

    });
}

function destroiCarrosel() {
    $('.carousel').slick('unslick');
}


const filtroOcioso = (elemento) => {
    return elemento.status.toLowerCase() == "ocioso";
}


const filtroParado = (elemento) => {
    return elemento.status.toLowerCase() == "parado";
}

const filtroAtivo = (elemento) => {
    return elemento.status.toLowerCase() == "ativo";
}

const mostrarTodos = (elemento) => {
    return elemento;
}

const filtros = {
    "mostrarTodos": mostrarTodos,
    "filtroAtivo": filtroAtivo,
    "filtroParado": filtroParado,
    "filtroOcioso": filtroOcioso
}


let searchName = document.getElementById('searchString');

searchName.onkeyup = () => {
    destroiCarrosel();

    var listaMaquinasFiltrada = listaMaquinas.filter(filtroNome);

    console.log(listaMaquinasFiltrada)
    resetaIntervals();
    mostraLista(listaMaquinasFiltrada);
}

const filtroNome = (elemento) => {
    return elemento.nome.toLowerCase().includes(searchName.value);
}


const listaOpcoes = document.getElementById('opcoes');

listaOpcoes.onchange = () => {
    destroiCarrosel()
    let listaMaquinasFiltrada = listaMaquinas.filter(filtros[listaOpcoes.value]);
    resetaIntervals();
    mostraLista(listaMaquinasFiltrada);
}
