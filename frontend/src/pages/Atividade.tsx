import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../services/api";
import IAtividade from "../interfaces/IAtividade";
import ButtonAppBar from "../components/ButtonAppBar";
import SimpleCard from "../components/SimpleCard";
import { TextField } from "@mui/material";
import { Button } from "@mui/material";
import Footer from "../components/Footer";
import './styles/atividade.css';
import moment from "moment";
import { toast } from "react-toastify";


type Atividade = IAtividade | null;

export default function Atividade() {
    const { id } = useParams();
    const [atividade, setAtividade] = useState<Atividade>(null);
    const [nomeAtividade, setNomeAtividade] = useState<String>("")
    const [descricaoAtividade, setDescricaoAtividade] = useState<String>("")

    const navigate = useNavigate();

    const carregarAtividade = async () => {
        await api.get(`api/Atividade/${id}`)
            .then(resp => {
                setAtividade(resp.data);
                setDescricaoAtividade(resp.data.descricaoAtividade)
                setNomeAtividade(resp.data.nomeAtividade);
            })
            .catch(error => {
                console.error(error);
                navigate("/")
            });
    }

    const atualizarAtividade = async () => {
        await api.put("api/Atividade", {
            atividadeId: atividade?.atividadeId,
            nomeAtividade: nomeAtividade,
            descricaoAtividade: descricaoAtividade,
            inicioAtividade: atividade?.inicioAtividade,
            finalAtividade: atividade?.finalAtividade,
        })
            .then(resp => {
                toast.success(resp.data);
                navigate("/atividades");
            })
            .catch(error => {
                toast.error("Erro ao tentar atualizar a atividade!");
                console.error(error);
            });
    }

    useEffect(() => {
        carregarAtividade();
    }, [])

    return (
        <React.Fragment>
            <div className="page-atividade">
                <ButtonAppBar>
                    <h1>Visualizar Atividade</h1>
                    <SimpleCard>
                        <p>Nome da Atividade:</p>
                        <TextField sx={{
                            width: "100%"
                        }}
                            value={nomeAtividade}
                            onChange={e => setNomeAtividade(e.target.value)}
                        />
                        <p>Descrição da Atividade:</p>
                        <TextField sx={{
                            width: "100%"
                        }}
                            value={descricaoAtividade}
                            onChange={e=> setDescricaoAtividade(e.target.value)}
                            multiline
                            rows={3}
                            inputProps={{ maxLength: 255 }}
                        />
                        <p>Início da Atividade:</p>
                        <TextField sx={{
                            width: "100%"
                        }}
                            value={moment(atividade?.inicioAtividade).format("DD/MM/YYYY HH:mm:ss")}
                            InputProps={{
                                readOnly: true,
                            }}
                        />

                        <p>Final da Atividade:</p>
                        <TextField sx={{
                            width: "100%"
                        }}
                            value={moment(atividade?.finalAtividade).format("DD/MM/YYYY HH:mm:ss")}
                            InputProps={{
                                readOnly: true,
                            }}
                        />

                        <p>Categoria da Atividade:</p>
                        <TextField sx={{
                            width: "100%"
                        }}
                            value={atividade?.nomeCategoria}
                            InputProps={{
                                readOnly: true,
                            }}
                        />

                        <div className="grupo-botoes">
                            <Button
                                variant="contained"
                                onClick={() => atualizarAtividade()}>
                                Salvar
                            </Button>
                            <Button onClick={()=> navigate("/atividades")}>Voltar</Button>
                        </div>
                    </SimpleCard>
                </ButtonAppBar>

                <Footer />
            </div>
        </React.Fragment>
    );
}