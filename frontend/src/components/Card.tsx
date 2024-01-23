import React, { ReactNode } from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';

interface ICard {
    children: ReactNode;
}

export default function OutlinedCard({ children }: ICard) {
    return (
        <React.Fragment>
            <Card variant="outlined" sx={{
                padding: "5px"
            }}>
                <CardContent sx={{
                    margin: "24px"
                }}>
                    {children}
                </CardContent>
            </Card>
        </React.Fragment>
    );
}