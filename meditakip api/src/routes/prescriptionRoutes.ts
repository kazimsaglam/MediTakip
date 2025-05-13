import { Router } from 'express';
import { prescriptionList, createPrescription  } from '../controllers/prescriptionController';

const router = Router();

router.get('/list/:code?', prescriptionList);
router.post('/create', createPrescription);

export default router;