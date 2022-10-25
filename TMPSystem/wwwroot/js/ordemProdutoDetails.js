const btnListar = document.getElementById("btnListarProcessos");
const secaoProcesso = document.getElementById("secaoProcesso");

let estadoBotaoListar = false;
btnListar.onclick = () => {
    estadoBotaoListar = !estadoBotaoListar;
    if (estadoBotaoListar) {
        chamaSessao();
        btnListar.innerHTML = " ";
        btnListar.append("ESCONDER PROCESSOS");
    } else {
        secaoProcesso.classList.remove("scale-in-ver-center");
        secaoProcesso.classList.add("scale-out-vertical");
        setTimeout(escondeSessao, 1000);
        btnListar.innerHTML = " ";
        btnListar.append("LISTAR PROCESSOS");
    }


}

function chamaSessao() {
    secaoProcesso.classList.add("secaoRegistrosParadas");
    secaoProcesso.classList.remove("secaoHidden");
    secaoProcesso.classList.add("scale-in-ver-center");
}

function escondeSessao() {
    secaoProcesso.classList.remove("secaoRegistrosParadas");
    secaoProcesso.classList.remove("scale-out-vertical");
    secaoProcesso.classList.add("secaoHidden");

}