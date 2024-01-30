export default interface ICategoria{
    categoriaId : number;
    nomeCategoria : String; 
    descricaoCategoria : String;
    dataCriacaoCategoria : Date; 
    dataAlteracaoCategoria : Date | null; 
}