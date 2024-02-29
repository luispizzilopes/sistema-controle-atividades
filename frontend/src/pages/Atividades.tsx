import React, { useEffect, useState } from "react";
import ButtonAppBar from "../components/ButtonAppBar";
import IAtividade from "../interfaces/IAtividade";
import { DataGrid, GridColDef, GridEventListener } from '@mui/x-data-grid';
import { useNavigate } from "react-router-dom";
import api from "../services/api";
import moment from "moment";
import './styles/atividades.css'
import { Button, Typography } from "@mui/material";
import SentimentDissatisfiedIcon from '@mui/icons-material/SentimentDissatisfied';
import SimpleCard from "../components/SimpleCard";
import WorkIcon from '@mui/icons-material/Work';
import Footer from "../components/Footer";
import { encryptText, decryptText } from "../Encrypt/Encrypt";

type Atividade = [IAtividade] | [];

export default function Atividades() {
    const [atividades, setAtividades] = useState<Atividade>([]);
    const navigate = useNavigate();

    const carregarAtividades = async () => {
        await api.get(`api/Atividade/${JSON.parse(decryptText(sessionStorage.getItem("session")!)).id}`)
            .then(resp => setAtividades(resp.data))
            .catch(error => {
                console.error(error);
                navigate("/");
            });
    }

    const localizedTextsMap = {
        columnMenuUnsort: "não classificado",
        columnMenuSortAsc: "Classificar por ordem crescente",
        columnMenuSortDesc: "Classificar por ordem decrescente",
        columnMenuFilter: "Filtro",
        columnMenuHideColumn: "Ocultar",
        columnMenuShowColumns: "Mostrar colunas",
        columnMenuManageColumns: "Administrar colunas"
    };

    const columns: GridColDef[] = [
        { field: 'atividadeId', headerName: 'Id', flex: 1 },
        { field: 'id', headerName: 'Id', flex: 1 },
        { field: 'nomeAtividade', headerName: 'Nome Atividade', flex: 2 },
        { field: 'descricaoAtividade', headerName: 'Descrição Atividade', flex: 2 },
        { field: 'inicioAtividade', headerName: 'Início da Atividade', flex: 3 },
        { field: 'finalAtividade', headerName: 'Final da Atividade', flex: 3 },
        { field: 'nomeCategoria', headerName: 'Categoria', flex: 2 },
    ];

    const rows = atividades.map((atividade, index) => ({
        atividadeId: atividade.atividadeId,
        id: index + 1, 
        nomeAtividade: atividade.nomeAtividade,
        descricaoAtividade: atividade.descricaoAtividade,
        inicioAtividade: moment(atividade.inicioAtividade).format("DD/MM/YYYY HH:mm:ss"),
        finalAtividade: moment(atividade.finalAtividade).format("DD/MM/YYYY HH:mm:ss"),
        nomeCategoria: atividade.nomeCategoria,
    }));

    const handleRowClick: GridEventListener<'rowClick'> = (params) => {
        navigate(`/atividade/${encryptText(params.row.atividadeId.toString())}`)
    };

    useEffect(() => {
        carregarAtividades();
    }, [])

    return (
        <React.Fragment>
            <div className="atividades-page">
                <ButtonAppBar>
                    <div>
                        <div className="cabecalho-atividades">
                            <div style={{ display: "flex", alignItems: "center" }}>
                                <WorkIcon style={{ marginRight: "10px" }} />
                                <h1>Atividades Cadastradas</h1>
                            </div>
                            <Button variant="contained" onClick={() => navigate("/nova-atividade")}>Nova Atividade</Button>
                        </div>
                        {atividades && atividades.length > 0 ?
                            <div style={{ height: '100%', width: '100%', overflowX: "auto" }}>
                                <small>* Para visualizar ou editar uma atividade clique em cima de sua linha!</small>
                                <DataGrid
                                    onRowClick={handleRowClick}
                                    rows={rows}
                                    getRowId={(row) => row.atividadeId}
                                    columns={columns}
                                    localeText={localizedTextsMap}
                                    initialState={{
                                        columns: {
                                            columnVisibilityModel: {
                                              // Hide columns status and traderName, the other columns will remain visible
                                              atividadeId: false,
                                            }
                                        },
                                        pagination: {
                                            paginationModel: { page: 0, pageSize: 10 },
                                        },
                                    }}
                                    pageSizeOptions={[5, 10]}
                                    style={{ marginTop: "10px" }}
                                />
                            </div>
                            :
                            <SimpleCard>
                                <Typography sx={{
                                    fontSize: "18px",
                                    display: "flex",
                                    alignItems: "center",
                                    marginBottom: "10px"
                                }}>
                                    Nenhuma Atividade Cadastrada!
                                    <SentimentDissatisfiedIcon sx={{ marginLeft: "10px" }} />
                                </Typography>
                                <small>Para registrar uma nova atividade clique no botão "Nova Atividade".</small>
                            </SimpleCard>
                        }
                    </div>
                </ButtonAppBar>

                <Footer />
            </div>
        </React.Fragment >
    );
}