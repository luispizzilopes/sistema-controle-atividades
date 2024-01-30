import React, { useEffect, useState } from "react";
import ButtonAppBar from "../components/ButtonAppBar";
import IAtividade from "../interfaces/IAtividade";
import { DataGrid, GridColDef, GridEventListener } from '@mui/x-data-grid';
import { useNavigate } from "react-router-dom";
import api from "../services/api";
import moment from "moment";
import './styles/atividades.css'
import { Button } from "@mui/material";

type Atividade = [IAtividade] | [];

export default function Atividades() {
    const [atividades, setAtividades] = useState<Atividade>([]);
    const navigate = useNavigate();

    const carregarAtividades = async () => {
        await api.get("api/Atividade")
            .then(resp => setAtividades(resp.data))
            .catch(error => console.error(error));
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
        { field: 'nomeAtividade', headerName: 'Nome Atividade', flex: 2 },
        { field: 'descricaoAtividade', headerName: 'Descrição Atividade', flex: 2 },
        { field: 'inicioAtividade', headerName: 'Início da Atividade', flex: 3 },
        { field: 'finalAtividade', headerName: 'Final da Atividade', flex: 3 },
        { field: 'nomeCategoria', headerName: 'Categoria', flex: 2 },
    ];
    

    const rows = atividades.map((atividade) => ({
        atividadeId: atividade.atividadeId,
        nomeAtividade: atividade.nomeAtividade,
        descricaoAtividade: atividade.descricaoAtividade,
        inicioAtividade: moment(atividade.inicioAtividade).format("DD/MM/YYYY hh:mm:ss"),
        finalAtividade: moment(atividade.finalAtividade).format("DD/MM/YYYY hh:mm:ss"),
        nomeCategoria: atividade.nomeCategoria,
    }));

    const handleRowClick: GridEventListener<'rowClick'> = (params) => {
        navigate(`/atividade/${params.row.atividadeId}`)
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
                            <h1>Atividades Cadastradas</h1>
                            <Button variant="contained" onClick={()=> navigate("/nova-atividade")}>Nova Atividade</Button>
                        </div>
                        <div style={{ height: '100%', width: '100%', overflowX: "auto" }}>
                            <DataGrid
                                onRowClick={handleRowClick}
                                rows={rows}
                                getRowId={(row) => row.atividadeId}
                                columns={columns}
                                localeText={localizedTextsMap}
                                initialState={{
                                    pagination: {
                                        paginationModel: { page: 0, pageSize: 10 },
                                    },
                                }}
                                pageSizeOptions={[5, 10]}
                            />
                        </div>
                    </div>
                </ButtonAppBar>
            </div>
        </React.Fragment >
    );
}