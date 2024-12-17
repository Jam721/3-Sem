from operator import itemgetter
class Microprocessor:
    def __init__(self,id,count,price,comp_id):
        self.id=id
        self.count=count
        self.price=price
        self.comp_id=comp_id

class Computer:
    def __init__(self,id,name):
        self.id=id
        self.name=name

class MicrComp:
    def __init__(self,micr_id,comp_id):
        self.micr_id=micr_id
        self.comp_id=comp_id

computers=[
    Computer(1,'Компьютер 1'),
    Computer(2,'Компьютер 2'),
    Computer(3,'Компьютер 3'),
    Computer(4,'Компьютер 4'),
    Computer(5,'Компьютер 5'),
    Computer(6,'Компьютер 6'),
]

microprocessors=[
    Microprocessor(1,1001,12000,2),
    Microprocessor(2,270011, 12442,3),
    Microprocessor(3,323312,147977,1),
    Microprocessor(4,664623,2356,3),
    Microprocessor(5,374223,2467,4),
    Microprocessor(6,12654,2357,5),
]

micr_comp=[
    MicrComp(1,1),
    MicrComp(2,2),
    MicrComp(3,3),
    MicrComp(3,4),
    MicrComp(4,4),
    MicrComp(5,6),
    MicrComp(5,2),
    MicrComp(1,2),
]

def main():

    one_to_many=[(m.count,m.price,c.name)
                 for m in microprocessors
                 for c in computers
                 if  m.comp_id==c.id]
    many_to_many_temp = [(c.name,mc.micr_id,mc.comp_id)
                         for c in computers
                         for mc in micr_comp
                         if c.id==mc.comp_id]
    many_to_many = [(m.count, m.price, c.name)
                    for mc in micr_comp
                    for m in microprocessors if m.id == mc.micr_id
                    for c in computers if c.id == mc.comp_id]
    
    print('Задание Г1')
    res_1 = {}
    for c in computers:
        if int(c.name[-1]) < 4:
            m_c = [(microprocessor.count, microprocessor.price) for microprocessor in microprocessors if microprocessor.comp_id == c.id]
            res_1[c.name] = m_c
    print(res_1)

    print ('Задание Г2')
    res_2=[]
    for c in computers:
        c_micrs=list(filter(lambda i:i[2]==c.name,one_to_many))
        if len(c_micrs)>0:
            s_price=[price for _,price, _ in c_micrs]
            s_max=max(s_price)
            res_2.append((c.name,s_max))
    res_2=sorted(res_2,key=itemgetter(1),reverse=True)
    print(res_2)

    print('Задание Г3')
    res_3=sorted(many_to_many,key=itemgetter(2))
    print(res_3)

if __name__ =='__main__':
    main()