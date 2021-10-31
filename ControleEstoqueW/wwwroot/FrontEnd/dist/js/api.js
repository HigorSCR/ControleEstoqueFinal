
function fazPost(url, body) {
    console.log("Body=", body)
    let request = new XMLHttpRequest()
    request.open("POST", url, true)
    request.setRequestHeader("Content-type", "application/json")
    request.send(JSON.stringify(body))

    request.onload = function () {
        console.log(this.responseText)
    }

    return request.responseText
}


function adicionarProduto() {
    event.preventDefault()
    let url = "http://127.0.0.1:5000/api/v1/produtos"
    let descricao = document.getElementById("descricao").value
    console.log(descricao)

    body = {
        "descricao": descricao
    }

    fazPost(url, body)
}