import { Router } from 'express';
import { drugsList, drugStockList, getRecommendDrugs } from '../controllers/drugController';

const router = Router();

router.get('/list', drugsList);
router.get('/stock/list', drugStockList);
router.post('/recommend', getRecommendDrugs)
export default router;