import express from 'express';
import morgan from 'morgan';
import dotenv from 'dotenv';
import cors from 'cors';
import authRoutes from './routes/authRoutes';
import doctorRoutes from './routes/doctorRoutes';
import drugRoutes from './routes/drugRoutes';
import prescriptionRoutes from './routes/prescriptionRoutes';
import { connectDB } from './config/db';

dotenv.config();

const app = express();

app.use(express.json());
//app.use(morgan('dev'));
app.use(cors());
connectDB();

app.use('/api/auth/', authRoutes);
app.use('/api/doctor/', doctorRoutes);
app.use('/api/drug/', drugRoutes);
app.use('/api/prescription/', prescriptionRoutes);

app.use((req, res) => {
  res.status(404).json({ data: 'Path error' });
});

export default app;