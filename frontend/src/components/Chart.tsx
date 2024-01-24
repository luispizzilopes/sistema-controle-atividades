import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';
import IChartProps from '../interfaces/IChartProps';

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

export const options = {
    responsive: true,
    plugins: {
        legend: {
            position: 'top' as const,
        },
        title: {
            display: true,
            text: `Gráfico Anual (${new Date().getFullYear()})`,
        },
    },
};

const labels = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];

export default function Chart({ graficoAtividades, graficoCategorias }: IChartProps) {
    const data = {
        labels,
        datasets: [
            {
                label: 'Atividades',
                data: [graficoAtividades[0], graficoAtividades[1], graficoAtividades[2], graficoAtividades[3], graficoAtividades[4], graficoAtividades[5],
                graficoAtividades[6], graficoAtividades[7], graficoAtividades[8], graficoAtividades[9], graficoAtividades[10], graficoAtividades[11]],
                backgroundColor: '#60246C',
            },
            {
                label: 'Categoria',
                data: [graficoCategorias[0], graficoCategorias[1], graficoCategorias[2], graficoCategorias[3], graficoCategorias[4], graficoCategorias[5],
                graficoCategorias[6], graficoCategorias[7], graficoCategorias[8], graficoCategorias[9], graficoCategorias[10], graficoCategorias[11]],
                backgroundColor: 'rgba(53, 162, 235, 0.5)',
            },
        ],
    };

    return <Bar options={options} data={data} style={{ marginTop: "20px" }} />;
}

