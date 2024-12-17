import unittest
from main import Computer, Microprocessor, MicrComp, get_one_to_many, get_many_to_many, task_g1, task_g2, task_g3

class TestMicroprocessorFunctions(unittest.TestCase):
    def setUp(self):
        self.computers = [
            Computer(1, 'Компьютер 1'),
            Computer(2, 'Компьютер 2'),
            Computer(3, 'Компьютер 3'),
            Computer(4, 'Компьютер 4'),
            Computer(5, 'Компьютер 5'),
            Computer(6, 'Компьютер 6'),
        ]

        self.microprocessors = [
            Microprocessor(1, 1001, 12000, 2),
            Microprocessor(2, 270011, 12442, 3),
            Microprocessor(3, 323312, 147977, 1),
            Microprocessor(4, 664623, 2356, 3),
            Microprocessor(5, 374223, 2467, 4),
            Microprocessor(6, 12654, 2357, 5),
        ]

        self.micr_comp = [
            MicrComp(1, 1),
            MicrComp(2, 2),
            MicrComp(3, 3),
            MicrComp(3, 4),
            MicrComp(4, 4),
            MicrComp(5, 6),
            MicrComp(5, 2),
            MicrComp(1, 2),
        ]

    def test_get_one_to_many(self):
        result = get_one_to_many(self.microprocessors, self.computers)
        self.assertTrue(len(result) > 0)
        self.assertIn((1001, 12000, 'Компьютер 2'), result)

    def test_get_many_to_many(self):
        result = get_many_to_many(self.microprocessors, self.computers, self.micr_comp)
        self.assertTrue(len(result) > 0)
        self.assertIn((1001, 12000, 'Компьютер 1'), result)

    def test_task_g1(self):
        result = task_g1(self.computers, self.microprocessors)
        self.assertIn('Компьютер 1', result)

    def test_task_g2(self):
        one_to_many = get_one_to_many(self.microprocessors, self.computers)
        result = task_g2(self.computers, one_to_many)
        self.assertTrue(result[0][1] > result[-1][1])

    def test_task_g3(self):
        many_to_many = get_many_to_many(self.microprocessors, self.computers, self.micr_comp)
        result = task_g3(many_to_many)
        self.assertTrue(len(result) > 0)

if __name__ == '__main__':
    unittest.main()
