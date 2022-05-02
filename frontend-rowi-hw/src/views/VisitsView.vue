<script>
import { ref } from "vue";
import { HTTP } from "../api/http-common";
import VisitsItem from "../components/VisitsItem.vue";

export default {
  components: { VisitsItem },
  data() {
    return {
      date: "",
      visits: [],
      doctors: [],
      medicines: [],
      patients: [],
      errors: [],
      newVisit: 0,
      dataRange: ref(),
      sortedVisits: [],
    };
  },
  props: ["id"],

  created() {
    this.init();
  },
  mounted() {},
  methods: {
    init() {
      this.getPatients();
      this.getVisits();
      this.getMedicines();
      this.getDoctors();
    },
    dateChanged() {
      console.log(this.date);
      return this.date;
    },
    getPatients() {
      HTTP.get(`/api/patients`)
        .then((response) => {
          this.patients = response.data;
          return this.patients;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getMedicines() {
      HTTP.get(`/api/medicines`)
        .then((response) => {
          this.medicines = response.data;
          return this.medicines;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getVisits() {
      HTTP.get(`/api/visits`)
        .then((response) => {
          this.visits = response.data;
          console.log(this.visits.filter(this.$route.params.id));
          return this.visits;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getDoctors() {
      HTTP.get(`/api/doctors`)
        .then((response) => {
          this.doctors = response.data;
          return this.doctors;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    async save() {
      let response = await HTTP.post(`/api/visits`, {
        visitDate: this.visitDate,
        visitPlace: this.visitPlace,
        patient: this.patient,
        doctor: this.doctor,
        medicine: this.medicine,
        diagnosis: this.diagnosis,
        recommendation: this.recommendation,
      });
      if (response.data) {
        this.$toast.success(`Created.`);
        setTimeout(this.$toast.clear, 500);
      }
    },
    getVisitsByDate() {
      if (!this.dataRange) {
        this.$toast.error(`Select search date Range.`);
      } else {
        this.sortedVisits = this.visits.filter(
          (visit) =>
            (new Date(visit.visitDate).getTime() >=
              new Date(this.dataRange[0]).getTime()) &
            (new Date(visit.visitDate).getTime() <=
              new Date(this.dataRange[1]).getTime())
        );
        if (this.sortedVisits.length) {
          this.$toast.success(`Filtered.`);
        } else {
          this.$toast.error(`Nothing to show.`);
        }
      }
    },
    modalClose() {
      this.newVisit = 0;
    },
  },
};
</script>

<template>
  <div>
    <div class="details flex justify-center">
      <button
        class="bg-green-500 hover:bg-green-900 px-2 py-2 rounded-md flex text-white"
        @click="this.newVisit = 1"
      >
        <i class="px-1 self-center"><DocumentationIcon /></i>
        New Visit
      </button>
    </div>
    <div class="py-4 flex justify-between w-4/5 m-auto">
      <span class="w-3/10">Search visit by DateRange</span>
      <Datepicker class="w-2/5" v-model="this.dataRange" range />
      <button
        class="w-1/5 bg-green-400 rounded-md text-white hover:bg-green-700"
        @click="getVisitsByDate"
      >
        search
      </button>
    </div>
    <div class="flex flex-col" v-if="!this.$route.params.id">
      <div v-if="this.sortedVisits && this.dataRange">
        <span v-for="visit in this.sortedVisits" :key="visit.id" class="w-full">
          <VisitsItem
            :data="visit"
            :patients="this.patients"
            :doctors="this.doctors"
            :medicines="this.medicines"
          />
        </span>
      </div>
      <VisitsItem
        v-else
        v-for="visit of this.visits"
        :key="visit.id"
        :data="visit"
        :patients="this.patients"
        :doctors="this.doctors"
        :medicines="this.medicines"
      />
    </div>
    <div class="flex flex-col" v-else>
      <div>
        <span v-for="visit in this.visits" :key="visit.id" class="w-full">
          <VisitsItem
            :data="visit"
            :patients="this.patients"
            :doctors="this.doctors"
            :medicines="this.medicines"
            v-if="visit.id == this.$route.params.id"
          />
        </span>
      </div>
    </div>

    <div
      v-if="this.newVisit"
      class="invisible0 fixed z-10 inset-0 overflow-y-auto"
      aria-labelledby="modal-title"
      role="dialog"
      aria-modal="true"
    >
      <div
        class="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0"
      >
        <div
          class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"
          aria-hidden="true"
        ></div>

        <span
          class="hidden sm:inline-block sm:align-middle sm:h-screen"
          aria-hidden="true"
          >&#8203;</span
        >

        <div
          class="relative inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full"
        >
          <div class="mt-10 sm:mt-0">
            <div class="px-12 py-6">
              <VisitsItem
                :data="{}"
                :popup="1"
                :patients="this.patients"
                :doctors="this.doctors"
                :medicines="this.medicines"
                @modalClose="this.modalClose()"
                @visitsRefresh="this.getVisits()"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
