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


function mostraLista(data)
{
    var divLista = document.getElementById('divMaquinas');
    
    for (let i = 0; i < data.length; i++) {
        divLista.innerHTML += `<div class="card card-escuro mb-3" style="max-width: 20rem;">
  <div class="card-header">${data[i].nome}</div>
  <div class="card-body">
    <h4 class="card-title"></h4>
    <p class="card-text">teste.</p>
<a href="/Maquinas/Details/${data[i].id}" class="btn btn-sm btn-outline-dark">Máquina Detalhes</a>
  </div>`
    }

}