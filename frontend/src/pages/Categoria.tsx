import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../services/api";
import ButtonAppBar from "../components/ButtonAppBar";
import SimpleCard from "../components/SimpleCard";
import { TextField } from "@mui/material";
import { Button } from "@mui/material";
import { decryptText } from "../Encrypt/Encrypt";
import './styles/categoria.css';
import moment from "moment";
import { toast } from "react-toastify";
import ICategoria from "../interfaces/ICategoria";
import Footer from "../components/Footer";


type Categoria = ICategoria | null;

export default function Categoria() {
    const { id } = useParams();
    const [categoria, setCategoria] = useState<Categoria>(null);
    const [nomeCategoria, setNomeCategoria] = useState<String>("")
    const [descricaoCategoria, setDescricaoCategoria] = useState<String>("")

    const navigate = useNavigate();

    const carregarCategoria = async () => {
        await api.get(`api/Categoria/${JSON.parse(decryptText(sessionStorage.getItem("session")!)).id}/${id}`)
            .then(resp => {
                setCategoria(resp.data);
                setDescricaoCategoria(resp.data.descricaoCategoria)
                setNomeCategoria(resp.data.nomeCategoria);
            })
            .catch(error => {
                console.error(error);
                navigate("/");
            });
    }

    const atualizarCategoria = async () => {
        await api.put("api/Categoria", {
            categoriaId: categoria?.categoriaId,
            userId: JSON.parse(decryptText(sessionStorage.getItem("session")!)).id, 
            nomeCategoria: nomeCategoria,
            descricaoCategoria: descricaoCategoria,
            dataCriacaoCategoria: categoria?.dataCriacaoCategoria,
            dataAlteracaoCategoria: new Date(),
        })
            .then(resp => {
                toast.success(resp.data);
                navigate("/categorias");
            })
            .catch(error => {
                toast.error("Erro ao tentar atualizar a categoria!");
                console.error(error);
            });
    }

    useEffect(() => {
        carregarCategoria();
    }, [])

    return (
        <React.Fragment>
            <div className="page-categoria">
                <ButtonAppBar>
                    <h1>Visualizar Categoria</h1>
                    <SimpleCard>
                        <p>Nome da Categoria:</p>
                        <TextField sx={{
                            width: "100%"
                        }}
                            value={nomeCategoria}
                            onChange={e => setNomeCategoria(e.target.value)}
                        />
                        <p>Descrição da Categoria:</p>
                        <TextField sx={{
                            width: "100%"
                        }}
                            value={descricaoCategoria}
                            onChange={e=> setDescricaoCategoria(e.target.value)}
                            multiline
                            rows={3}
                            inputProps={{ maxLength: 255 }}
                        />
                        <p>Data de Criação:</p>
                        <TextField sx={{
                            width: "100%"
                        }}
                            value={moment(categoria?.dataCriacaoCategoria).format("DD/MM/YYYY HH:mm:ss")}
                            InputProps={{
                                readOnly: true,
                            }}
                        />

                        <p>Data da Última Atualização:</p>
                        <TextField sx={{
                            width: "100%"
                        }}
                            value={categoria?.dataAlteracaoCategoria != undefined ? moment(categoria?.dataAlteracaoCategoria).format("DD/MM/YYYY HH:mm:ss") : ""}
                            InputProps={{
                                readOnly: true,
                            }}
                        />

                        <div className="grupo-botoes">
                            <Button
                                variant="contained"
                                onClick={() => atualizarCategoria()}>
                                Salvar
                            </Button>
                            <Button onClick={()=> navigate("/categorias")}>Voltar</Button>
                        </div>
                    </SimpleCard>
                </ButtonAppBar>

                <Footer/>
            </div>
        </React.Fragment>
    );
}