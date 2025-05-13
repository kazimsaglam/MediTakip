import { Router } from 'express';
import { PatientList } from '../controllers/doctorController';

const router = Router();

router.get('/patient/list', PatientList);

export default router;