import React, { useEffect, useState } from "react";
import ButtonAppBar from "../components/ButtonAppBar";
import { DataGrid, GridColDef, GridEventListener } from '@mui/x-data-grid';
import { useNavigate } from "react-router-dom";
import api from "../services/api";
import moment from "moment";
import './styles/categorias.css'
import { Button, Typography } from "@mui/material";
import SentimentDissatisfiedIcon from '@mui/icons-material/SentimentDissatisfied';
import SimpleCard from "../components/SimpleCard";
import ICategoria from "../interfaces/ICategoria";
import DialogNovaCategoria from "../components/DialogNovaCategoria";
import CategoryIcon from '@mui/icons-material/Category';
import Footer from "../components/Footer";
import { decryptText } from "../Encrypt/Encrypt";

type Categoria = [ICategoria] | [];

export default function Categorias() {
    const [categorias, setCategorias] = useState<Categoria>([]);
    const [open, setOpen] = useState<boolean>(false)
    const navigate = useNavigate();

    const carregarCategorias = async () => {
        await api.get(`api/Categoria/${JSON.parse(decryptText(sessionStorage.getItem("session")!)).id}`)
            .then(resp => setCategorias(resp.data))
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
        { field: 'categoriaId', headerName: 'Id', flex: 1 },
        { field: 'nomeCategoria', headerName: 'Nome Categoria', flex: 2 },
        { field: 'descricaoCategoria', headerName: 'Descrição Categoria', flex: 3 },
        { field: 'dataCriacaoCategoria', headerName: 'Data de Cadastro', flex: 3 },
        { field: 'dataAlteracaoCategoria', headerName: 'Data de Alteração', flex: 3 },
    ];

    const rows = categorias.map((categoria) => ({
        categoriaId: categoria.categoriaId,
        nomeCategoria: categoria.nomeCategoria,
        descricaoCategoria: categoria.descricaoCategoria,
        dataCriacaoCategoria: moment(categoria.dataCriacaoCategoria).format("DD/MM/YYYY HH:mm:ss"),
        dataAlteracaoCategoria: categoria.dataAlteracaoCategoria != undefined ? moment(categoria.dataAlteracaoCategoria).format("DD/MM/YYYY HH:mm:ss") : "-",
    }));

    const handleRowClick: GridEventListener<'rowClick'> = (params) => {
        navigate(`/categoria/${params.row.categoriaId}`)
    };

    useEffect(() => {
        carregarCategorias();
    }, [])

    return (
        <React.Fragment>
            <div className="categorias-page">
                <ButtonAppBar>
                    <div>
                        <div className="cabecalho-categorias">
                            <div style={{ display: "flex", alignItems: "center" }}>
                                <CategoryIcon style={{ marginRight: "10px" }} />
                                <h1>Categorias Cadastradas</h1>
                            </div>
                            <Button variant="contained" onClick={() => setOpen(true)}>Nova Categoria</Button>
                        </div>
                        {categorias && categorias.length > 0 ?
                            <div style={{ height: '100%', width: '100%', overflowX: "auto" }}>
                                <small>* Para visualizar ou editar uma categoria clique em cima de sua linha!</small>
                                <DataGrid
                                    onRowClick={handleRowClick}
                                    rows={rows}
                                    getRowId={(row) => row.categoriaId}
                                    columns={columns}
                                    localeText={localizedTextsMap}
                                    initialState={{
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
                                    Nenhuma Categoria Cadastrada!
                                    <SentimentDissatisfiedIcon sx={{ marginLeft: "10px" }} />
                                </Typography>
                                <small>Para registrar uma nova Categoria clique no botão "Nova Categoria".</small>
                            </SimpleCard>
                        }
                    </div>
                </ButtonAppBar>
            </div>

            <DialogNovaCategoria open={open} setOpen={setOpen} carregarCategorias={carregarCategorias} />
            <Footer/>
        </React.Fragment >
    );
}