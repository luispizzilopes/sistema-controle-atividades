import React, { ReactNode } from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';

interface ICard {
    children: ReactNode;
}

export default function SimpleCard({ children }: ICard) {
    return (
        <React.Fragment>
            <Card sx={{
                padding: "0px",
                width: "100%"
            }}>
                <CardContent sx={{
                           padding: "24px",
                    margin: "5px"
                }}>
                    {children}
                </CardContent>
            </Card>
        </React.Fragment>
    );
}