import React, { useEffect, useState } from "react";
import ButtonAppBar from "../components/ButtonAppBar";
import SimpleCard from "../components/SimpleCard";
import { TextField } from "@mui/material";
import MenuItem from '@mui/material/MenuItem';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import FormControl from '@mui/material/FormControl';
import ICategoria from "../interfaces/ICategoria";
import api from "../services/api";
import { Button } from "@mui/material";
import { toast } from "react-toastify";
import CircleProgressBar from "../components/CircleProgressBar";
import moment from "moment";
import Alert from '@mui/material/Alert';
import Stack from '@mui/material/Stack';
import './styles/nova-atividade.css'
import { decryptText } from "../Encrypt/Encrypt";
import { useNavigate } from "react-router-dom";
import Footer from "../components/Footer";

type Categoria = [ICategoria] | [];

export default function NovaAtividade() {
    const [categorias, setCategorias] = useState<Categoria>([]);

    const [nomeAtividade, setNomeAtividade] = useState<String>("");
    const [categoria, setCategoria] = useState<String>("");
    const [descricaoAtividade, setDescricaoAtividade] = useState<String>("");
    const [dataInicio, setDataInicio] = useState<Date | null>(null);

    const [etapa, setEtapa] = useState<number>(1);
    const [tempoDeAtividade, setTempoDeAtividade] = useState<number>(0);

    const [aviso, setAviso] = useState<boolean>(true);

    const navigate = useNavigate();

    const carregarCategorias = async () => {
        await api.get(`api/Categoria/${JSON.parse(decryptText(sessionStorage.getItem("session")!)).id}`)
            .then(resp => setCategorias(resp.data))
            .catch(error => {
                console.log(error);
                navigate("/");
            })
    }

    const handleCategoria = (event: SelectChangeEvent) => {
        setCategoria(event.target.value as String);
    };

    const handleEtapa = () => {
        if (categoria != "" && categoria != null && nomeAtividade != "" && nomeAtividade != null) {
            setEtapa(2);
            let dataInicio = new Date();
            setDataInicio(dataInicio);
        } else {
            toast.warn("Verifique todos os campos e tente novamente!");
        }
    }

    const enviarNovaAtividade = async () => {
        await api.post("api/Atividade", {
            nomeAtividade: nomeAtividade,
            userId: JSON.parse(decryptText(sessionStorage.getItem("session")!)).id,
            descricaoAtividade: descricaoAtividade,
            inicioAtividade: dataInicio,
            finalAtividade: new Date(),
            categoriaId: categoria
        })
            .then(resp => {
                toast.success(resp.data);
                navigate("/atividades");
            })
            .catch(error => {
                console.error(error);
                toast.error("Erro ao registrar uma nova atividade");
            })
    }

    function converterSegundosParaFormatoDeHoras(segundos: number): string {
        let horas: number = Math.floor(segundos / 3600);
        let minutos: number = Math.floor((segundos % 3600) / 60);
        let segundosRestantes: number = segundos % 60;

        const formatadoHoras: string = horas < 10 ? '0' + horas : horas.toString();
        const formatadoMinutos: string = minutos < 10 ? '0' + minutos : minutos.toString();
        const formatadoSegundos: string = segundosRestantes < 10 ? '0' + segundosRestantes : segundosRestantes.toString();

        return `${formatadoHoras}:${formatadoMinutos}:${formatadoSegundos}`;
    }

    useEffect(() => {
        setTimeout(() => {
            let novoValorTempoAtividade = tempoDeAtividade + 1;
            setTempoDeAtividade(novoValorTempoAtividade);
        }, 1000);
    })

    useEffect(() => {
        carregarCategorias();
    }, [])

    return (
        <React.Fragment>
            <div className="nova-atividade-page">
                <ButtonAppBar>
                    <h1>Nova Atividade</h1>
                    <div>
                        <div style={{ display: etapa == 1 ? "block" : "none" }}>
                            <SimpleCard>
                                <h3 style={{ margin: 0, marginBottom: "20px" }}>Preencha as informações da Atividade</h3>

                                <p>Nome da Atividade</p>
                                <TextField variant="outlined"
                                    sx={{
                                        width: "100%"
                                    }}
                                    value={nomeAtividade}
                                    onChange={e => setNomeAtividade(e.target.value)}
                                />

                                <p>Categoria da Atividade</p>
                                <FormControl fullWidth>
                                    <Select
                                        labelId="categoria-select"
                                        id="categoria-select"
                                        onChange={handleCategoria}
                                    >
                                        {categorias.map(categoria =>
                                            <MenuItem
                                                key={categoria.categoriaId}
                                                value={categoria.categoriaId}>
                                                {categoria.nomeCategoria}
                                            </MenuItem>
                                        )}
                                    </Select>
                                </FormControl>

                                <p>Descrição da Atividade</p>
                                <TextField
                                    value={descricaoAtividade}
                                    onChange={e => setDescricaoAtividade(e.target.value)}
                                    multiline
                                    rows={3}
                                    inputProps={{ maxLength: 255 }}
                                    sx={{
                                        width: "100%"
                                    }}
                                />


                                <Button
                                    onClick={() => handleEtapa()}
                                    variant="contained"
                                    sx={{
                                        width: "100%",
                                        marginTop: "20px"
                                    }}>
                                    Continuar
                                </Button>
                            </SimpleCard>
                        </div>

                        <div style={{ display: etapa == 2 ? "flex" : "none" }} className="etapa-dois-atividade">
                            <div className="contador-tempo-atividade">
                                <CircleProgressBar />
                                <p>Tempo da Atividade: {converterSegundosParaFormatoDeHoras(tempoDeAtividade)}</p>
                            </div>
                            <div className="informacoes-atividade">
                                <p><b> Nome da Atividade: </b>{nomeAtividade}</p>
                                <p><b> Categoria da Atividade: </b>{categorias.find(c => c.categoriaId.toString() == categoria)?.nomeCategoria}</p>
                                <p><b> Início da Atividade: </b>{moment(dataInicio).format("DD/MM/YYYY HH:mm:ss")}</p>
                                <p><b> Descrição da Atividade: </b>{descricaoAtividade}</p>
                                <Button
                                    variant="contained"
                                    onClick={() => navigate("/home")}>
                                    Cancelar
                                </Button>
                                <Button
                                    variant="contained"
                                    onClick={() => enviarNovaAtividade()}
                                >
                                    Finalizar
                                </Button>
                            </div>
                            <Stack sx={{ width: '100%', marginTop: "20px", display: aviso === true ? "flex" : "none" }} spacing={2}>
                                <Alert severity="warning" onClose={() => setAviso(false)}>
                                    Não saia desta tela nem feche o seu navegador; caso contrário, perderá o progresso feito até agora.
                                </Alert>
                            </Stack>
                        </div>
                    </div>
                </ButtonAppBar>
                <Footer/>
            </div>
        </React.Fragment >
    );
}