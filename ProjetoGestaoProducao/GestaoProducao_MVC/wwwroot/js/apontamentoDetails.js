const btnListar = document.getElementById("btnListarParadas");
const secaoParadas = document.getElementById("secaoParadas");

let estadoBotaoListar = false;
btnListar.onclick = () => {
    estadoBotaoListar = !estadoBotaoListar;
    if (estadoBotaoListar) {
        chamaSessao();
        btnListar.innerHTML = " ";
        btnListar.append("ESCONDER PARADAS");
    } else {
        secaoParadas.classList.remove("scale-in-ver-center");
        secaoParadas.classList.add("scale-out-vertical");
        setTimeout(escondeSessao, 1000);
        btnListar.innerHTML = " ";
        btnListar.append("LISTAR PARADAS");
    }
   

}

function chamaSessao() {
    secaoParadas.classList.add("secaoRegistrosParadas");
    secaoParadas.classList.remove("secaoHidden");
    secaoParadas.classList.add("scale-in-ver-center");
}

function escondeSessao() {
    secaoParadas.classList.remove("secaoRegistrosParadas");
    secaoParadas.classList.remove("scale-out-vertical");
    secaoParadas.classList.add("secaoHidden");
    
}